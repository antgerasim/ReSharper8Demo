using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;

namespace ERPStore.Workflows.Activities
{
	public partial class IncrementPageActivity : Activity
	{
		public IncrementPageActivity()
		{
			InitializeComponent();
		}

		public static DependencyProperty PageIndexProperty =
			DependencyProperty.Register("PageIndex", typeof(int),
			typeof(IncrementPageActivity));

		public int PageIndex
		{
			get
			{
				return (int)base.GetValue(PageIndexProperty);
			}
			set
			{
				base.SetValue(PageIndexProperty, value);
			}
		}

		protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
		{
			PageIndex++;
			return ActivityExecutionStatus.Closed;
		}
	}
}
