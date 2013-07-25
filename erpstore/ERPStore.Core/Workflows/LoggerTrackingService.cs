using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Workflow.Runtime.Tracking;
using System.Collections.Specialized;
using System.Workflow.ComponentModel;

namespace ERPStore.Workflows
{
	public sealed class LoggerTrackingService : TrackingService
	{
		private Logging.ILogger m_Logger;

		public LoggerTrackingService(Logging.ILogger logger)
		{
			m_Logger = logger;
		}

		protected override bool TryGetProfile(Type workflowType, out TrackingProfile profile)
		{
			//Depending on the workflowType, service can return different tracking profiles
			//In this sample we're returning the same profile for all running types
			profile = GetProfile();
			return true;
		}

		protected override TrackingProfile GetProfile(Guid workflowInstanceId)
		{
			// Does not support reloading/instance profiles
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		protected override TrackingProfile GetProfile(Type workflowType, Version profileVersionId)
		{
			// Return the version of the tracking profile that runtime requests (profileVersionId)
			return GetProfile();
		}

		protected override bool TryReloadProfile(Type workflowType, Guid workflowInstanceId, out TrackingProfile profile)
		{
			// Returning false to indicate there is no new profiles
			profile = null;
			return false;
		}

		protected override TrackingChannel GetTrackingChannel(TrackingParameters parameters)
		{
			//return a tracking channel to receive runtime events
			return new Workflows.LoggerTrackingChannel(parameters, m_Logger);
		}

		// Profile creation
		private static TrackingProfile GetProfile()
		{
			// Create a Tracking Profile
			var profile = new TrackingProfile();
			profile.Version = new Version("3.0.0");

			// Add a TrackPoint to cover all activity status events
			var activityTrackPoint = new ActivityTrackPoint();
			var activityLocation = new ActivityTrackingLocation(typeof(Activity));
			activityLocation.MatchDerivedTypes = true;
			var wLocation = new WorkflowTrackingLocation();

			var statuses = Enum.GetValues(typeof(ActivityExecutionStatus)) as IEnumerable<ActivityExecutionStatus>;
			foreach (ActivityExecutionStatus status in statuses)
			{
				activityLocation.ExecutionStatusEvents.Add(status);
			}

			activityTrackPoint.MatchingLocations.Add(activityLocation);
			profile.ActivityTrackPoints.Add(activityTrackPoint);

			// Add a TrackPoint to cover all workflow status events
			var workflowTrackPoint = new WorkflowTrackPoint();
			workflowTrackPoint.MatchingLocation = new WorkflowTrackingLocation();
			foreach (TrackingWorkflowEvent workflowEvent in Enum.GetValues(typeof(TrackingWorkflowEvent)))
			{
				workflowTrackPoint.MatchingLocation.Events.Add(workflowEvent);
			}
			profile.WorkflowTrackPoints.Add(workflowTrackPoint);

			// Add a TrackPoint to cover all user track points
			var userTrackPoint = new UserTrackPoint();
			var userLocation = new UserTrackingLocation();
			userLocation.ActivityType = typeof(Activity);
			userLocation.MatchDerivedActivityTypes = true;
			userLocation.ArgumentType = typeof(object);
			userLocation.MatchDerivedArgumentTypes = true;
			userTrackPoint.MatchingLocations.Add(userLocation);
			profile.UserTrackPoints.Add(userTrackPoint);

			return profile;
		}
	}
}
