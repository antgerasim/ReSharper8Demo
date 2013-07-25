using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Runtime.Hosting;
using System.Workflow.Runtime;

using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace ERPStore.Workflows
{
	/// <summary>
	/// Methode liées à l'execution de workflows
	/// </summary>
	public static class WorkflowHelper
	{
		private static System.Workflow.Runtime.WorkflowRuntime m_Runtime;

		/// <summary>
		/// Execute un workflow de manière manuelle, le thread est bloqué 
		/// jusqu'a la fin.
		/// </summary>
		/// <param name="workflowType">Type of the workflow.</param>
		/// <param name="properties">The properties.</param>
		public static void ExecuteManualWorkflow(Type workflowType, Dictionary<string, object> properties)
		{
			if (m_Runtime == null)
			{
				m_Runtime = CreateRuntime();
				var logger = DependencyResolver.Current.GetService<Logging.ILogger>();
				m_Runtime.AddService(new Workflows.LoggerTrackingService(logger));
			}
			var manualScheduler = m_Runtime.GetService<ManualWorkflowSchedulerService>();

			var instance = m_Runtime.CreateWorkflow(workflowType, properties);
			
			Exception ex = null;

			EventHandler<WorkflowCompletedEventArgs> completedHandler = null;
			completedHandler = (o, e) =>
			{
				if (e.WorkflowInstance.InstanceId != instance.InstanceId)
				{
					return;
				}
				m_Runtime.WorkflowCompleted -= completedHandler;

				var enumerator = e.OutputParameters.GetEnumerator();
				while (enumerator.MoveNext())
				{
					var pair = enumerator.Current;
					if (properties != null
						&& properties.ContainsKey(pair.Key))
					{
						properties[pair.Key] = pair.Value;
					}
				}
			};

			EventHandler<WorkflowTerminatedEventArgs> terminatedHandler = null;
			terminatedHandler = delegate(object o, WorkflowTerminatedEventArgs e)
			{
				if (e.WorkflowInstance.InstanceId != instance.InstanceId)
				{
					return;
				}
				m_Runtime.WorkflowTerminated -= terminatedHandler;

				ex = e.Exception;
			};

			m_Runtime.WorkflowCompleted += completedHandler;

			instance.Start();
			manualScheduler.RunWorkflow(instance.InstanceId);

			if (ex != null)
			{
				throw ex;
			}
		}

		private static System.Workflow.Runtime.WorkflowRuntime CreateRuntime()
		{
			var runtime = new System.Workflow.Runtime.WorkflowRuntime();
			var manualService = new ManualWorkflowSchedulerService();

			runtime.AddService(manualService);

			return runtime;
		}
	}
}
