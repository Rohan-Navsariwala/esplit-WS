using System;
using System.Collections.Generic;
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
	}
}
