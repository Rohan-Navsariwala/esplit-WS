using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types
{
	public  class Notification
	{
		public int NotificationID { get; set; }
		public int NotifyFor { get; set; }
		public string ActionPerformedBy { get; set; }
		public string NotificationText { get; set; }
		public NotificationType NotificationType { get; set; }
		public bool isDeleted { get; set; }
	}

	public enum NotificationType
	{
		[Description("TEST")]
		Test,

		[Description("SYSTEM")]
		System,

		[Description("SPLIT_CREATED")]
		Split_Created,

		[Description("SPLIT_APPROVAL")]
		Split_Approval,

		[Description("SPLIT_REJECTED")]
		Split_Rejected,

		[Description("SPLIT_PAYMENT")]
		Split_Payment,

		[Description("SPLIT_DELETE")]
		Split_Delete,

		[Description("CONNECTION_SENT")]
		Connection_Sent,

		[Description("CONNECTION_ACCEPTED")]
		Connection_Accepted,

		[Description("CONNECTION_DELETED")]
		Connection_Deleted,

		[Description("CONNECTION_REQUESTED")]
		Connection_Requested
	}
}
