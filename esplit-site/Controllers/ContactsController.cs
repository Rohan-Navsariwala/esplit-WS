using Biz.Services;
using Common.Utils;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace esplit_site.Controllers
{
	[Route("{controller}")]
	public class ContactsController : Controller
	{
		public ContactService contactService;
		public ContactsController(CacheService cache, NotificationService notificationService, Identity common)
		{
			contactService = new ContactService(cache, notificationService, common);
			ViewData["username"] = common.GetClaims().username;
		}

		[Authorize]
		public IActionResult Index()
		{
			List<List<ContactDto>> allContacts = new List<List<ContactDto>>();
			allContacts.Add(contactService.GetContacts(ContactStatus.APPROVED) ?? null);
			allContacts.Add(contactService.GetContacts(ContactStatus.PENDING, ContactRequestDirection.RECEIVED) ?? null);
			allContacts.Add(contactService.GetContacts(ContactStatus.PENDING, ContactRequestDirection.SENT) ?? null);

			return View(allContacts);
		}

		[Authorize]
		[HttpPost]
		[Route("SendRequest")]
		public IActionResult SendRequest(string toUserName)
		{
			int ContactID = contactService.CreateContact(toUserName);
			if (ContactID > 0)
			{
				ContactDto contact = contactService.GetThisContact(ContactID, ContactStatus.PENDING);
				return PartialView("_ContactCard", contact);
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to send contact request." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("ApproveRequest")]
		public IActionResult ApproveRequest(int ContactID)
		{
			if(contactService.InteractContact(ContactID, ContactStatus.APPROVED))
			{
				ContactDto contact = contactService.GetThisContact(ContactID, ContactStatus.APPROVED);
				return PartialView("_ContactCard", contact);
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to approve contact request." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("RejectRequest")]
		public IActionResult RejectRequest(int ContactID)
		{
			if (contactService.InteractContact(ContactID, ContactStatus.REJECTED))
			{
				return Ok(new { success = true, message = "Contact request rejected successfully." });

			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to reject contact request." });
			}
		}

		[Authorize]
		[HttpDelete]
		[Route("DeleteRequest")]
		public IActionResult DeleteRequest(int ContactID, int type)
		{
			if (contactService.DeleteContact(ContactID, type))
			{
				return Ok(new { success = true, message = "Contact deleted successfully." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to delete contact." });
			}
		}
	}
}
