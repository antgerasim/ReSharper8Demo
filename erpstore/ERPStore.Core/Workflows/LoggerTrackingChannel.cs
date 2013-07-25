using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Runtime.Tracking;
using System.Diagnostics;

namespace ERPStore.Workflows
{
	/// <summary>
	/// Receives tracking events for a specific instance.
	/// </summary>
	public class LoggerTrackingChannel : TrackingChannel
	{
		private Logging.ILogger m_Logger;
		private TrackingParameters m_Parameters;

		protected LoggerTrackingChannel()
		{
		}

		public LoggerTrackingChannel(TrackingParameters parameters, Logging.ILogger logger)
		{
			m_Logger = logger;
			m_Parameters = parameters;
		}

		/// <summary>
		/// Receives tracking events.  Instance terminated events are written to the event log.
		/// </summary>
		protected override void Send(TrackingRecord record)
		{
			if (record is WorkflowTrackingRecord)
			{
				WriteWorkflowTrackingRecord((WorkflowTrackingRecord)record);
			}
			if (record is ActivityTrackingRecord)
			{
				WriteActivityTrackingRecord((ActivityTrackingRecord)record);
			}
			if (record is UserTrackingRecord)
			{
				WriteUserTrackingRecord((UserTrackingRecord)record);
			}
		}

		protected override void InstanceCompletedOrTerminated()
		{
			GetTitle("Workflow Instance Completed or Terminated");
		}

		private string GetTitle(string title)
		{
			var result = title + "\r\n";
			return result;
		}

		private void WriteWorkflowTrackingRecord(WorkflowTrackingRecord workflowTrackingRecord)
		{
			var message = GetTitle("Workflow Tracking Record");
			message += string.Format("EventDateTime: {0}\r\n" , workflowTrackingRecord.EventDateTime);
			message += string.Format("Status: {0}\r\n" , workflowTrackingRecord.TrackingWorkflowEvent);
			m_Logger.Debug(message);			
		}

		private void WriteActivityTrackingRecord(ActivityTrackingRecord activityTrackingRecord)
		{
			var message = GetTitle("Activity Tracking Record");
			message += string.Format("EventDateTime: {0}\r\n" , activityTrackingRecord.EventDateTime);
			message += string.Format("QualifiedName: {0}\r\n" , activityTrackingRecord.QualifiedName);
			message += string.Format("Type: {0}\r\n" , activityTrackingRecord.ActivityType);
			message += string.Format("Status: {0}\r\n" , activityTrackingRecord.ExecutionStatus);
			foreach (var item in activityTrackingRecord.Body)
			{
				message += string.Format("{0}: {1}\r\n", item.FieldName, item.Data);
			}
			m_Logger.Debug(message);
		}

		private void WriteUserTrackingRecord(UserTrackingRecord userTrackingRecord)
		{
			var message = GetTitle("User Activity Record");
			message += string.Format("EventDateTime: {0}\r\n" , userTrackingRecord.EventDateTime);
			message += string.Format("QualifiedName: {0}\r\n", userTrackingRecord.QualifiedName);
			message += string.Format("ActivityType: {0}\r\n", userTrackingRecord.ActivityType.FullName);
			message += string.Format("Args: {0}\r\n", userTrackingRecord.UserData);
			if (userTrackingRecord.UserDataKey != null)
			{
				switch (userTrackingRecord.UserDataKey.ToLower())
				{
					case "error":
					case "fatal":
						if (userTrackingRecord.UserData is Exception)
						{
							message += "\r\n";
							message += ((Exception)userTrackingRecord.UserData).ToString();
							m_Logger.Error(userTrackingRecord.UserData as Exception);
						}
						m_Logger.Error(message);
						break;
					case "notification":
						m_Logger.Notification(message);
						break;
					default:
						m_Logger.Debug(message);
						break;
				}
			}
			else
			{
				m_Logger.Debug(message);
			}
		}
	}
}
