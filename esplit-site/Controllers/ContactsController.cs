using Biz.Services;
using Common.Utils;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace esplit_site.Controllers
{
	public class ContactsController : Controller
	{
		public ContactService contactService;
		public ContactsController(CacheService cache, NotificationService notificationService, Identity common)
		{
			contactService = new ContactService(cache, notificationService, common);
		}

		[Authorize]
		public IActionResult Index()
		{
			List<ContactDto> contacts = contactService.GetContacts(ContactStatus.APPROVED);

			return View(contacts);
		}

		[Authorize]
		[HttpPost]
		[Route("SendRequest")]
		public IActionResult SendRequest(string toUserName)
		{
			if (contactService.CreateContact(toUserName))
			{
				return Ok(new { success = true, message = "Contact request sent successfully." });
			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to send contact request." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("ApproveRequest")]
		public IActionResult Approve(int ContactID)
		{
			if(contactService.InteractContact(ContactID, ContactStatus.APPROVED))
			{
				return Ok(new { success = true, message = "Contact request approved successfully." });

			}
			else
			{
				return BadRequest(new { success = false, message = "Failed to approve contact request." });
			}
		}

		[Authorize]
		[HttpPatch]
		[Route("RejectRequest")]
		public IActionResult Reject(int ContactID)
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
		public IActionResult Delete(int ContactID)
		{
			if (contactService.DeleteContact(ContactID))
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
