using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Common.Types;

namespace DataAccess.Repositories
{
	public class SplitRepository
	{
		public bool AddSplitParticipant(List<SplitContact> splitContacts)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitID", splitContacts.First().SplitID },
				{ "SplitParticipantID", splitContacts.First().SplitParticipantID },
				{ "OweAmount", splitContacts.First().OweAmount }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("AddSplitParticipant", parameters);
		}

		public int CreateSplit(SplitInfo split)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", split.CreatedBy },
				{ "SplitAmount", split.SplitAmount },
				{ "SplitDescription", split.SplitDescription }
			};
			return (int)DataAccess.dbMethods.DbUpdate("CreateSplit", parameters, true);
		}

		public bool DeleteSplitParticipant(int userID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("DeleteSplitParticipant", parameters);
		}

		public List<ParticipantDto> GetSplitParticipant(int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "SplitID", splitID }
			};
			return DataAccess.dbMethods.DbSelect<ParticipantDto>("GetSplitParticipant", parameters, reader =>
			{
				return new ParticipantDto {
					SplitParticipant = new SplitContact {
						SplitID = (int)reader["SplitID"],
						SplitParticipantID = (int)reader["SplitParticipantID"],
						OweAmount = (decimal)reader["OweAmount"],
						SplitStatus = (SplitStatus)reader["SplitStatus"],
						StatusUpdateOn = Convert.ToDateTime(reader["StatusUpdateOn"]),
					},
					UserData = new User {
						UserID = (int)reader["UserID"],
						FullName = reader["FullName"].ToString(),
						UserName = reader["UserName"].ToString(),
						CreatedAt = (DateTime)reader["CreatedAt"],
						IsActive = Convert.ToBoolean(reader["IsActive"])
					}
				};
			});
		}

		public List<SplitInfo> GetSplits(int userID, SplitStatus splitStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitStatus", splitStatus }
			};
			return DataAccess.dbMethods.DbSelect<SplitInfo>("GetSplits", parameters, reader =>
			{
				return new SplitInfo {
					SplitID = (int)reader["SplitID"],
					CreatedBy = (int)reader["CreatedBy"],
					SplitAmount = (decimal)reader["SplitAmount"],
					SplitDescription = reader["SplitDescription"].ToString(),
					CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
					UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]),
					IsClosed = Convert.ToBoolean(reader["IsClosed"]),
				};
			});
		}

		public bool PayDue(int userID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("PayDue", parameters);
		}

		public bool ToggleSplit(int userID, int splitID, SplitStatus splitStatus)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID },
				{ "SplitStatus", splitStatus }
			};
			return (bool)DataAccess.dbMethods.DbUpdate("ToggleSplit", parameters);
		}

		public bool MarkClosed(int userID, int splitID)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>()
			{
				{ "UserID", userID },
				{ "SplitID", splitID },
			};

			return (bool)DataAccess.dbMethods.DbUpdate("MarkClosed", parameters);
		}
	}
}
