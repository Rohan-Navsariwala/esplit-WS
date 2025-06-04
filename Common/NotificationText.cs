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
		public static readonly string ConnectionRejected = "The Connection request has been rejected by ";
		public static readonly string ConnectionAccepted = "The Connection request has been accepted by ";
		public static readonly string ConnectionDeleted = "Successfully Deleted connection with ";
		public static readonly string ConnectionCreated = "The Connection request is sent to ";
		public static readonly string ConnectionRequested = "You have a connection request from ";
		public static readonly string AddedAsParticipant = "You have been added as split participant by ";
		public static readonly string NewParticipantAdded = " have been added as split participant";
		public static readonly string UserHasPaid = " has paid the amount for split";
		public static readonly string CloseSplit = "The split has been closed";
		public static readonly string SplitCreated = "A Split has been created";
	}
}
