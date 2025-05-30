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
		TEST,
		SYSTEM,

		SPLIT_CREATED,
		SPLIT_APPROVAL,
		SPLIT_REJECTED,
		SPLIT_DELETE,
		SPLIT_PAYMENT,

		CONNECTION_SENT,
		CONNECTION_ACCEPTED,
		CONNECTION_REJECTED,
		CONNECTION_DELETED,
		CONNECTION_REQUESTED
	}
}
