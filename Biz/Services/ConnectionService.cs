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
			//later we have to add authorization checks
			return contactRepo.GetConnections(userID, connectionStatus);
		}
	}
}
