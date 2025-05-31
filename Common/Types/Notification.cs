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
		public bool IsDeleted { get; set; }
	}

	public enum NotificationType
	{
		TEST = 0,
		SYSTEM = 1,

		SPLIT_CREATED = 11,
		SPLIT_APPROVAL = 12,
		SPLIT_REJECTED = 13,
		SPLIT_DELETE = 14,
		SPLIT_PAYMENT = 15,

		SPLIT_PARTICIPANT_ADDED = 21,

		CONNECTION_SENT = 31,
		CONNECTION_ACCEPTED = 32,
		CONNECTION_REJECTED = 33,
		CONNECTION_DELETED = 34,
		CONNECTION_REQUESTED = 35
	}
}
