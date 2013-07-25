using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public interface IScheduledTaskService
	{
		void Start();
		void Stop();
		void Add(Models.TaskEntry task);
		// For tests
		void ManualExecuteTask(Models.TaskEntry task);
		int GetTaskCount();
		bool Contains(string taskName);
	}
}
