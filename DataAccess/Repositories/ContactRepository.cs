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
		/// <summary>
		/// this methods creates and initiates a connection
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="toUserName"></param>
		/// <returns></returns>
		public int CreateContact(int userID, string toUserName)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "toUserName", toUserName }
			};
			return Convert.ToInt32(DataAccess.dbMethods.DbUpdate("CreateContact", parameters, true));
		}

		/// <summary>
		/// this method deletes a connection
		/// </summary>
		/// <param name="contactID"></param>
		/// <returns></returns>
		public bool DeleteContact(int contactID) 
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "ContactID", contactID }
			};
			return Convert.ToBoolean(DataAccess.dbMethods.DbUpdate("DeleteContact", parameters));
		}

		/// <summary>
		/// this method gets all the connections for given userID
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="contactStatus"></param>
		/// <returns></returns>
		public List<ContactDto> GetContacts(int userID, ContactStatus contactStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "UserID", userID },
				{ "ContactStatus", contactStatus }
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
						ContactInit = Convert.ToDateTime(reader["ContactInit"]),
						StatusUpdateOn = reader["StatusUpdateOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["StatusUpdateOn"]),
						ContactStatus = (ContactStatus)reader["ContactStatus"],
						UserID1 = (int)reader["UserID1"],
						UserID2 = (int)reader["UserID2"]
					}
				};
			});
		}

		/// <summary>
		/// this method used to toggle connection options
		/// </summary>
		/// <param name="contactID"></param>
		/// <param name="contactStatus"></param>
		/// <returns></returns>
		public int InteractContact(int contactID, ContactStatus contactStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "ContactID", contactID },
				{ "ContactStatus", contactStatus }
			};
			return (int)DataAccess.dbMethods.DbUpdate("InteractContact", parameters, true);
		}

		/// <summary>
		/// this method is not recommended to use, it is incomplete
		/// </summary>
		/// <param name="contactID"></param>
		/// <returns></returns>
		public Contact GetThisContact(int contactID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object> 
			{ 
				{ "ContactID", contactID }
			};
			return null;
		}
	}
}
