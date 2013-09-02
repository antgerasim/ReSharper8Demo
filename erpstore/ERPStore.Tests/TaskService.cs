using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Tests
{
	public class TaskService : ERPStore.Services.ScheduledTaskService
	{
		public TaskService(ERPStore.Logging.ILogger logger )
			: base(logger)
		{

		}

		public override void Start()
		{
			
		}

		public override void Stop()
		{
			
		}

		public override void Add(ERPStore.Models.TaskEntry task)
		{
			this.ManualExecuteTask(task);
		}
	}
}
