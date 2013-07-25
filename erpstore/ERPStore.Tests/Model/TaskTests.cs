using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ERPStore.Tests.Models
{
	[TestFixture]
	public class TaskTests
	{
		// [Test]
		public void RunOnceByDay()
		{
			var task = new ERPStore.Models.TaskEntry();
			task.Period = ERPStore.Models.ScheduledTaskTimePeriod.Day;
			task.StartHour = 4;
			task.StartMinute = 1;
			task.Interval = 1;

			var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

			for (int i = 0; i < 46; i++)
			{
				var result = task.CanRun(start);
				if (i == 4 || i == 28)
				{
					Assert.IsTrue(result);
				}
				else
				{
					Assert.IsFalse(result);
				}
				start = start.AddHours(1);
			}
		}
	}
}
