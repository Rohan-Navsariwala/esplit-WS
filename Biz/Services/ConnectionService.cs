using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class ConnectionService
	{
		ContactRepository contactRepo;
		public ConnectionService() 
		{
			contactRepo = new ContactRepository();
		}

		public List<ConnectionDto> GetConnections(int userID, string connectionStatus)
		{
			//later we have to add authorization checks before returing any connections
			return contactRepo.GetConnections(userID, connectionStatus);
		}

		public int CreateConnection(int userID, string toUserName)
		{
			//auth checks
			return contactRepo.CreateConnection(userID, toUserName);
		}

		public bool DeleteConnection(int contactID)
		{
			//auth checks
			return contactRepo.DeleteConnection(contactID);
		}
		public bool InteractConnection(int contactID, string connectionStatus)
		{
			//auth checks
			return contactRepo.InteractConnection(contactID, connectionStatus);
		}
	}
}
