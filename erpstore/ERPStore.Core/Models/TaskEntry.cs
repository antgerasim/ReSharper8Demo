using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Tache de type workflow 
	/// </summary>
	[Serializable]
	public class TaskEntry : IDisposable
	{
		public TaskEntry()
		{
			NextRunningDate = DateTime.MinValue;
			IsRunning = false;
			CreationDate = DateTime.Now;
			IsDeleted = false;
		}

		public Guid Id { get; set; }
		public DateTime NextRunningDate { get; set; }
		public Type WorkflowType { get; set; }
		public Dictionary<string, object> WorkflowProperties { get; set; }
		public ScheduledTaskTimePeriod Period { get; set; }
		public int Interval { get; set; }
		public int StartDay { get; set; }
		public int StartHour { get; set; }
		public int StartMinute { get; set; }
		public int DelayedStart { get; set; }
		public bool IsRunning { get; set; }
		public Exception Exception { get; set; }
		public System.Threading.ThreadStart Started { get; set; }
		public System.Threading.ThreadStart Completed { get; set; }
		public System.Threading.ThreadStart Terminated { get; set; }
		public DateTime CreationDate { get; private set; }
		public DateTime StartDate { get; set; }
		public DateTime? TerminatedDate { get; set; }
		public string Name { get; set; }
		public bool IsDeleted { get; private set; }

		public void MarkAsDelete()
		{
			IsDeleted = true;
		}

		public bool CanRun()
		{
			return CanRun(DateTime.Now);
		}

		public bool CanRun(DateTime now)
		{
			if (IsRunning)
			{
				return false;
			}

			// Demarrage retardé
			if (now < CreationDate.AddSeconds(DelayedStart))
			{
				return false;
			}

			if (Period == ScheduledTaskTimePeriod.Once)
			{
				return true;
			}

			if (now >= NextRunningDate)
			{
				switch (Period)
				{
					// Pour l'instant pas de difference
					case ScheduledTaskTimePeriod.Month:

						NextRunningDate = new DateTime(now.Year, now.Month, StartDay, StartHour, StartMinute, 0).AddMonths(Interval);

						break;
					case ScheduledTaskTimePeriod.Day:

						NextRunningDate = new DateTime(now.Year, now.Month, now.Day, StartHour, StartMinute, 0).AddDays(Interval);
						break;

					case ScheduledTaskTimePeriod.WorkingDay:

						while(true)
						{
							NextRunningDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).AddDays(Interval);

							if (NextRunningDate.DayOfWeek == DayOfWeek.Saturday
								|| NextRunningDate.DayOfWeek == DayOfWeek.Sunday)
							{
								NextRunningDate = NextRunningDate.AddDays(1);
								break;
							}
						}

						break;
					case ScheduledTaskTimePeriod.Hour:

						NextRunningDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).AddHours(Interval);

						break;
					case ScheduledTaskTimePeriod.Minute:

						NextRunningDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(Interval);

						break;
				}

				return true;
			}
			return false;
		}


		#region IDisposable Members

		public void Dispose()
		{
			Started = null;
			Completed = null;
			Terminated = null;
			WorkflowProperties = null;
			WorkflowType = null;
		}

		#endregion
	}
}
