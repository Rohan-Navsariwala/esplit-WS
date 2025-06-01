using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public static class NotificationText
	{
		public static readonly string ConnectionRejected = "The Connection request has been rejected by User: ";
		public static readonly string ConnectionAccepted = "The Connection request has been accepted by User: ";
		public static readonly string ConnectionDeleted = "The Connection request has been deleted by User: ";
		public static readonly string ConnectionCreated = "The Connection request is sent to User: ";
		public static readonly string ConnectionRequested = "You have a connection request from ";
		public static readonly string AddedAsParticipant = "You have been added as split participant";
		public static readonly string NewParticipantAdded = "New Participant added successfully";
		public static readonly string UserHasPaid = "User has paid the amount for split";
		public static readonly string SplitPaid = "Split has been paid";
		public static readonly string CloseSplit = "The split has been closed";
		public static readonly string SplitCreated = "A Split has been created";
	}
}
