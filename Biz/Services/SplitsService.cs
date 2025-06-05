using DataAccess.Repositories;
using Common.Types;
using Common;
using static Azure.Core.HttpHeader;
using Common.Utils;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Biz.Services
{
	//- handle response for get user involved split when there is no incoming splits available
	//- for close split(it wont work if the cache is not populated, i.e.get owned splits not run already)
	//- for edit and pay due some null reference exception is thrown
	public class SplitsService
	{
		private readonly SplitRepository splitRepository;
		private readonly NotificationService notifyService;
		private readonly CacheService _cache;
		private readonly Identity _common;
		private readonly UserRepository userRepository;
		private readonly int _userID;
		private readonly string _userName;

		public SplitsService(NotificationService notifyService, CacheService cache, Identity common)
		{
			splitRepository = new SplitRepository();
			this.notifyService = notifyService;
			userRepository = new UserRepository();
			_cache = cache;
			_common = common;
			(_userName, _userID) = _common.GetClaims();
		}

		public List<SplitInfo> GetSplits(SplitStatus splitStatus)
		{
			
			List<SplitInfo> splits = splitRepository.GetSplits(_userID, splitStatus);
			string cacheKey = $"Split_{splitStatus}_{_userID}";

			return _cache.InsertIntoCache<List<SplitInfo>>(cacheKey, splits);

		}

		public List<ParticipantDto> GetParticipants(int splitID)
		{
			return splitRepository.GetSplitParticipant(splitID);
		}

		public bool CreateSplit(Split split)
		{
			split.Info.CreatedBy = _userID;
			int splitID = splitRepository.CreateSplit(split.Info);

			if (splitID > 0)
			{
				//as we have to update the cache for new split created, which will be utilized in the further process of adding participants
				string cacheKey = $"Split_{SplitStatus.OWNED}_{_userID}";
				List<SplitInfo> splits;
				try
				{
					splits = (List<SplitInfo>)_cache.GetFromCache(cacheKey);

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex); //there has to be better way to handle this exception
				}
				finally
				{
					splits = new List<SplitInfo>();
				}
				split.Info.SplitID = splitID;
				splits.Add(split.Info);
				_cache.InsertIntoCache(cacheKey, splits);


				foreach (SplitContact contact in split.Contacts)
				{
					contact.SplitID = splitID;
					contact.SplitStatus = SplitStatus.PENDING_APPROVAL;
					AddSplitParticipant(contact);
				}
			}

			Notification notification = new Notification() {
				NotifyFor = _userID,
				ActionPerformedBy = NotificationType.SYSTEM.ToString(),
				NotificationText = NotificationText.SplitCreated,
				NotificationType = NotificationType.SPLIT_CREATED
			};
			notifyService.CreateNotification(notification);
			return true;
		}

		public bool AddSplitParticipant(SplitContact splitContact)
		{
			if (IsOperationAllowed(splitContact.SplitID, SplitStatus.OWNED))
			{ //here we need one more check about if user is allowed to add that contact person or not // maybe we can utilize the is operation allowed method from contacts already
				if (splitRepository.AddSplitParticipant(splitContact))
				{
					Notification notification = new Notification()
					{
						NotifyFor = splitContact.SplitParticipantID,
						ActionPerformedBy = userRepository.GetUserNameByID(splitContact.SplitParticipantID),
						NotificationText = NotificationText.AddedAsParticipant + _userName,
						NotificationType = NotificationType.SPLIT_PARTICIPANT_ADDED
					};

					return true;
				}
				return false;
			}
			else
			{
				return false;
			}
		}

		public bool DeleteSplitParticipant(int userIDtoBeDeleted, int splitID)
		{
			if (IsOperationAllowed(splitID, SplitStatus.OWNED))
			{
				if (splitRepository.DeleteSplitParticipant(userIDtoBeDeleted, splitID))
				{
					Notification notification = new Notification()
					{
						NotifyFor = _userID,
						ActionPerformedBy = NotificationType.SYSTEM.ToString(),
						NotificationText = NotificationText.ConnectionDeleted + userRepository.GetUserNameByID(userIDtoBeDeleted),
						NotificationType = NotificationType.SPLIT_PARTICIPANT_DELETED
					};
					return true;
				}
				return false;
			}
			else
			{
				return false;
			}
		}

		public bool PayDue(int splitID)
		{	//we still have a vulnerability here of performing status change operation with togglesplit even after paydue since the cache is not updated for removing the split from the list, but that is not high priority and i'll do it later
			if (IsOperationAllowed(splitID, SplitStatus.ALL))
			{
				if (splitRepository.PayDue(_userID, splitID)) //we can also accomodate this to return split owner id
				{
					Notification notification = new Notification()
					{
						NotifyFor = 1, //split owner
						ActionPerformedBy = _userName,
						NotificationText = NotificationText.UserHasPaid,
						NotificationType = NotificationType.SPLIT_PAYMENT
					};
					notifyService.CreateNotification(notification);

					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public bool ToggleSplit(int splitID, string splitStatus)
		{
			//also utilize the common generic split with all status, the split status makes the difference, here the cases might be accept and reject
			SplitStatus status = (SplitStatus)Enum.Parse(typeof(SplitStatus), splitStatus);
			if(IsOperationAllowed(splitID, SplitStatus.ALL))
			{

				if(splitRepository.ToggleSplit(_userID, splitID, status)) //we can make this return the id of split owner
				{
					Notification notification = new Notification()
					{
						NotifyFor = 1, //owner of that split needs to be notified
						ActionPerformedBy = _userName,
						NotificationText = (status == SplitStatus.APPROVED_UNPAID) ? NotificationText.SplitRequestAccepted : NotificationText.SplitRequestRejected,
						NotificationType = (status == SplitStatus.APPROVED_UNPAID) ? NotificationType.SPLIT_APPROVAL : NotificationType.SPLIT_REJECTED
					};
					notifyService.CreateNotification(notification);

					string cacheKey = $"Split_{SplitStatus.ALL}_{_userID}";
					List<SplitInfo> splits;
					splits = (List<SplitInfo>)_cache.GetFromCache(cacheKey);
					splits.Add(new SplitInfo() { SplitID = splitID });
					_cache.InsertIntoCache(cacheKey, splits);

					return true;
				}
				return false;
			}
			else
			{
				return false;
			}


			//also update the cache straight after toggling the split
		}

		public bool MarkClosed(int splitID)
		{
			if (IsOperationAllowed(splitID, SplitStatus.OWNED))
			{
				if (splitRepository.MarkClosed(_userID, splitID))
				{
					Notification notification = new Notification()
					{
						NotifyFor = _userID, //notification needed to be sent to all the users about split is closed or just the logged in user that decide
						ActionPerformedBy = "SYSTEM", //this will change to current user if notify sent to all participants
						NotificationText = NotificationText.CloseSplit,
						NotificationType = NotificationType.SPLIT_CLOSED
					};
					notifyService.CreateNotification(notification);

					return true;
				}
				return false;
			}
			else
			{
				return false;
			}
		}

		public bool IsOperationAllowed(int SplitID, SplitStatus status)
		{
			//The split status can either be OWNED or ALL as we have cached data based on these 2 only
			string cacheKey = $"Split_{status}_{_userID}";
			List<SplitInfo> splits;

			try
			{
				splits = (List<SplitInfo>)_cache.GetFromCache(cacheKey);
			}
			catch (Exception e)
			{
				//rather than returning false, i can populate the cache
				Console.WriteLine(e);
				splits = _cache.InsertIntoCache(cacheKey, splitRepository.GetSplits(_userID, status));
			}

			if(splits?.Find(s => s.SplitID == SplitID) != null)
			{
				return true;
			} 
			else
			{
				return false;
			}
		}
	}
}
