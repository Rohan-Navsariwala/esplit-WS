using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Common.Types;
using System.Data.Common;

namespace DataAccess.Repositories
{
	public class ContactRepository
	{
		public int CreateContact(int userID, string toUserName)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "toUserName", toUserName }
			};
			return Convert.ToInt32(DataAccess.dbMethods.DbUpdate("CreateConnection", parameters, true));
		}

		public bool DeleteContact(int contactID) 
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "ContactID", contactID }
			};
			return Convert.ToBoolean(DataAccess.dbMethods.DbUpdate("DeleteContact", parameters));
		}

		public List<ContactDto> GetContacts(int userID, string connectionStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "ConnectionStatus", connectionStatus }
			};
			return DataAccess.dbMethods.DbSelect<ContactDto>("GetContacts", parameters, reader =>
			{
				return new ContactDto {
					UserData = new User {
						UserID = (int)reader["UserID"],
						UserName = reader["UserName"].ToString(),
						FullName = reader["FullName"].ToString(),
						CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
						IsActive = Convert.ToBoolean(reader["IsActive"]),
					},
					ContactData = new Contact {
						ContactID = (int)reader["ContactID"],
						ContactInit = Convert.ToDateTime(reader["ConnectionInit"]),
						StatucUpdateOn = reader["ApprovedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["ApprovedOn"]),
						ContactStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), reader["ConnectionStatus"].ToString()),
						UserID1 = (int)reader["UserID1"],
						UserID2 = (int)reader["UserID2"]
					}
				};
			});
		}

		public bool InteractContact(int contactID, string connectionStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "ContactID", contactID },
				{ "ConnectionStatus", connectionStatus }
			};
			return Convert.ToBoolean(DataAccess.dbMethods.DbUpdate("InteractConnection", parameters));
		}
	}
}
