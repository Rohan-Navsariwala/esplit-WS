using DataAccess.Repositories;
using Common.Types;
using Common;

namespace Biz.Services
{
	public class SplitsService
	{
		SplitRepository splitRepository;
		NotificationService notifyService;
		public SplitsService()
		{
			splitRepository = new SplitRepository();
			notifyService = new NotificationService();
		}

		public List<SplitInfo> GetSplits(int userID, SplitStatus splitStatus)
		{
			//SplitStatus status = (SplitStatus)Enum.Parse(typeof(SplitStatus), splitStatus);
			return splitRepository.GetSplits(userID, splitStatus);
		}

		public List<ParticipantDto> GetParticipants(int splitID)
		{
			return splitRepository.GetSplitParticipant(splitID);
		}

		public bool CreateSplit(Split split)
		{
			int splitID = splitRepository.CreateSplit(split.Info);
			if (splitID > 0)
			{
				foreach (SplitContact contact in split.Contacts)
				{
					contact.SplitID = splitID;
					contact.SplitStatus = SplitStatus.PENDING_APPROVAL;
					AddSplitParticipant(contact);
				}
			}
			Notification notification = new Notification() {
				NotifyFor = 0, //myself
				ActionPerformedBy = "SYSTEM",
				NotificationText = NotificationText.SplitCreated,
				NotificationType = NotificationType.SPLIT_CREATED
			};
			notifyService.CreateNotification(notification);
			return true;
		}

		public bool AddSplitParticipant(SplitContact splitContact)
		{
			if (splitRepository.AddSplitParticipant(splitContact))
			{
				Notification notification = new Notification() {
					NotifyFor = splitContact.SplitParticipantID,
					ActionPerformedBy = "user",
					NotificationText = NotificationText.AddedAsParticipant,
					NotificationType = NotificationType.SPLIT_PARTICIPANT_ADDED
				};
				if(notifyService.CreateNotification(notification) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}

		public bool DeleteSplitParticipant(int userID, int splitID)
		{
			if(splitRepository.DeleteSplitParticipant(userID, splitID))
			{
				Notification notification = new Notification() {
					NotifyFor = 0, //myself
					ActionPerformedBy = "SYSTEM",
					NotificationText = NotificationText.ConnectionDeleted,
					NotificationType = NotificationType.SPLIT_PARTICIPANT_DELETED
				};
				if (notifyService.CreateNotification(notification) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}

		public bool PayDue(int userID, int splitID)
		{
			if(splitRepository.PayDue(userID, splitID))
			{
				Notification notification = new Notification() {
					NotifyFor = 0, //split owner
					ActionPerformedBy = "me user",
					NotificationText = NotificationText.UserHasPaid,
					NotificationType = NotificationType.SPLIT_PAYMENT
				};
				notifyService.CreateNotification(notification);

				notification.NotifyFor = 0; //user who performed , myself
				notification.NotificationText = NotificationText.SplitPaid;
				notifyService.CreateNotification(notification);

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool ToggleSplit(int userID, int splitID, string splitStatus)
		{
			SplitStatus status = (SplitStatus)Enum.Parse(typeof(SplitStatus), splitStatus);
			return splitRepository.ToggleSplit(userID, splitID, status);
		}

		public bool MarkClosed(int userID, int splitID)
		{
			if(splitRepository.MarkClosed(userID, splitID))
			{
				Notification notification = new Notification() {
					NotifyFor = 0,
					ActionPerformedBy = "SYSTEM",
					NotificationText = NotificationText.CloseSplit,
					NotificationType = NotificationType.SPLIT_PARTICIPANT_ADDED
				};
				if (notifyService.CreateNotification(notification) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
	}
}
