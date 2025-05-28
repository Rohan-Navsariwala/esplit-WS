using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Common.Types;

namespace DataAccess.Repositories
{
	public class ContactRepository
	{
		private readonly string _connectionString;

		public ContactRepository() 
		{
			_connectionString = "";
		}

		public bool CreateConnection(int userID, string toUserName)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "toUserName", toUserName }
			};
			return DataAccess.dbMethods.DbUpdate("CreateConnection", parameters);
		}

		public bool DeleteConnection(int contactID) 
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "ContactID", contactID }
			};
			return DataAccess.dbMethods.DbUpdate("DeleteConnection", parameters);
		}

		public List<ConnectionDto> GetConnections(int userID, string connectionStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "ConnectionStatus", connectionStatus }
			};
			return DataAccess.dbMethods.DbSelect<ConnectionDto>("GetContacts", parameters, reader =>
			{
				return new ConnectionDto {
					UserData = new User {
						UserID = (int)reader["UserID"],
						UserName = reader["UserName"].ToString(),
						FullName = reader["FullName"].ToString(),
						CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
						IsActive = Convert.ToBoolean(reader["IsActive"]),
					},
					ContactData = new Contact {
						ContactID = (int)reader["ContactID"],
						ConnectionInit = Convert.ToDateTime(reader["ConnectionInit"]),
						ApprovedOn = Convert.ToDateTime(reader["ApprovedOn"]),
						ConnectionStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), reader["ConnectionStatus"].ToString()),
					}
				};
			});
		}

		public bool InteractConnection(int userID, string connectionStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "ConnectionStatus", connectionStatus }
			};
			return DataAccess.dbMethods.DbUpdate("InteractConnection", parameters);
		}
	}
}
