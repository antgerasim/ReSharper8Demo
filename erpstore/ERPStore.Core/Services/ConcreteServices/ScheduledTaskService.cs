using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Runtime;
using System.Timers;
using System.Workflow.Runtime.Hosting;

namespace ERPStore.Services
{
	public delegate void Workflow(Type workflowType, Dictionary<string, object> properties);

	/// <summary>
	/// Service de gestion des taches 
	/// </summary>
	public class ScheduledTaskService : IScheduledTaskService
	{
		WorkflowRuntime m_WFRuntime = null;
		Timer m_Timer;
		SynchronizedCollection<Models.TaskEntry> m_TaskList;

		public ScheduledTaskService(Logging.ILogger logger)
		{
			this.Logger = logger;
			m_TaskList = new SynchronizedCollection<Models.TaskEntry>();
		}

		protected Logging.ILogger Logger { get; set; }

		public virtual void Stop()
		{
			if (m_Timer != null)
			{
				m_Timer.Stop();
			}
			if (m_WFRuntime != null)
			{
				m_WFRuntime.StopRuntime();
				m_WFRuntime = null;
			}
		}

		public virtual void Start()
		{
			m_WFRuntime = new WorkflowRuntime();
			m_WFRuntime.AddService(new Workflows.LoggerTrackingService(Logger));

			m_WFRuntime.StartRuntime();

			m_Timer = new Timer(); 
			m_Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
			m_Timer.Start();
		}

		public bool Contains(string taskName)
		{
			bool result = false;
			if (m_TaskList.IsNullOrEmpty())
			{
				return false;
			}
			lock (m_TaskList.SyncRoot)
			{
				result = m_TaskList.Where(i => i.Name != null).Any(i => i.Name.Equals(taskName));
			}
			return result;
		}

		public virtual void Add(Models.TaskEntry task)
		{
			m_TaskList.Add(task);
			if (m_WFRuntime != null)
			{
				System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((object target) =>
					{
						ProcessTask(task);
					}));
			}
		}

		public void ManualExecuteTask(Models.TaskEntry task)
		{
			ERPStore.Workflows.WorkflowHelper.ExecuteManualWorkflow(task.WorkflowType, task.WorkflowProperties);
		}
		
		#region Private

		private void Timer_Elapsed(object sender, EventArgs e)
		{
			m_Timer.Interval = 1000 * 60; // toutes les minutes
			var task = GetFirstRunnableTask();
			if (task != null)
			{
				ProcessTask(task);
			}

			// Suppression des tâches terminées
			lock(m_TaskList.SyncRoot)
			{
				var removeList = m_TaskList.Where(i => i.Period == ERPStore.Models.ScheduledTaskTimePeriod.Once
											&& !i.IsRunning
											&& i.TerminatedDate.HasValue);

				foreach (var item in removeList)
				{
					item.Dispose();
					item.MarkAsDelete();
				}

				m_TaskList.RemoveAll(i => i.IsDeleted);
			}
		}

		/// <summary>
		/// Recupération de la première tache executable 
		/// dans l'ordre FIFO
		/// </summary>
		/// <returns></returns>
		private Models.TaskEntry GetFirstRunnableTask()
		{
			Models.TaskEntry result = null;
			lock (m_TaskList.SyncRoot)
			{
				result = m_TaskList.OrderBy(i => i.CreationDate).FirstOrDefault(i => i.CanRun());
			}
			return result;
		}

		private void ProcessTask(Models.TaskEntry task)
		{
			try
			{
				task.IsRunning = true;
				task.StartDate = DateTime.Now;
				task.Id = ExecuteWorkflow(task.WorkflowType, task.WorkflowProperties);
				if (task.Started != null)
				{
					task.Started.Invoke();
				}
			}
			catch (Exception ex)
			{
				task.IsRunning = false;
				Logger.Error(ex);
			}
		}

		private Guid ExecuteWorkflow(Type workflowType, Dictionary<string, object> properties)
		{
			Logger.Debug("Execute workflow {0}", workflowType);
			var instance = m_WFRuntime.CreateWorkflow(workflowType, properties);

			EventHandler<WorkflowEventArgs> startedHandler = null;
			startedHandler = (o, e) => 
			{
				if (e.WorkflowInstance.InstanceId != instance.InstanceId)
				{
					return;
				}
				m_WFRuntime.WorkflowStarted -= startedHandler;
				Logger.Info("Workflow {0} starting", instance.GetWorkflowDefinition().QualifiedName);
			};

			EventHandler<WorkflowCompletedEventArgs> completedHandler = null;
			completedHandler = (o, e) =>
			{
				if (e.WorkflowInstance.InstanceId != instance.InstanceId)
				{
					return;
				}
				m_WFRuntime.WorkflowCompleted -= completedHandler;
				Logger.Info("End of process workflow {0}", instance.InstanceId);

				Models.TaskEntry task = null;
				lock (m_TaskList.SyncRoot)
				{
					task = m_TaskList.SingleOrDefault(i => i.Id == instance.InstanceId);
				}
				if (task == null)
				{
					return;
				}

				var enumerator = e.OutputParameters.GetEnumerator();
				while (enumerator.MoveNext())
				{
					var pair = enumerator.Current;
					if (task.WorkflowProperties.ContainsKey(pair.Key))
					{
						task.WorkflowProperties[pair.Key] = pair.Value;
					}
				}

				task.IsRunning = false;

				if (task.Completed != null)
				{
					task.Completed.Invoke();
				}

				if (task.Period == ERPStore.Models.ScheduledTaskTimePeriod.Once)
				{
					m_TaskList.Remove(task);
				}
			};

			EventHandler<WorkflowTerminatedEventArgs> terminatedHandler = null;
			terminatedHandler = (o, e) =>
			{
				if (e.WorkflowInstance.InstanceId != instance.InstanceId)
				{
					return;
				}
				m_WFRuntime.WorkflowTerminated -= terminatedHandler;
				Logger.Info("Stop workflow process : {0}", instance.InstanceId.ToString());

				Models.TaskEntry task = null;
				lock (m_TaskList.SyncRoot)
				{
					task = m_TaskList.SingleOrDefault(i => i.Id == instance.InstanceId);
				}
				if (task == null)
				{
					return;
				}

				task.IsRunning = false;
				task.TerminatedDate = DateTime.Now;
				task.Exception = e.Exception;

				if (task.Exception != null)
				{
					Logger.Error(task.Exception);
				}

				if (task.Terminated != null)
				{
					task.Terminated.Invoke();
				}

				if (task.Period == ERPStore.Models.ScheduledTaskTimePeriod.Once)
				{
					m_TaskList.Remove(task);
				}
			};

			m_WFRuntime.WorkflowStarted += startedHandler;
			m_WFRuntime.WorkflowCompleted += completedHandler;
			m_WFRuntime.WorkflowTerminated += terminatedHandler;
			instance.Start();

			return instance.InstanceId;
		}

		public int GetTaskCount()
		{
			return m_TaskList.Count;
		}

		#endregion

	}
}
