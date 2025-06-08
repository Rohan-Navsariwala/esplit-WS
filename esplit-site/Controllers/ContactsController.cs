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

		public IActionResult SendRequest()
		{
			return Ok();
		}

		public IActionResult Approve()
		{
			return Ok();
		}
		public IActionResult Reject()
		{
			return Ok();
		}

		public IActionResult Delete()
		{
			return Ok();
		}
	}
}
