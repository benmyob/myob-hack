using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.UI.Repositories;
using MusicStore.Web.UI.ViewModels;

namespace MusicStore.Web.UI.Controllers
{
	[Route("play-music")]
    public class RegistrationController : Controller
	{
		private readonly TableRepository repository;

		public RegistrationController(TableRepository repository)
		{
			this.repository = repository;
		}

		[Route("register")]
		[HttpGet]
	    public IActionResult Register()
		{
			var model = new RegistrationViewModel();
			return View(model);
		}

		[Route("register")]
		[HttpPost]
	    public async Task<IActionResult> Register(RegistrationViewModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    if (await repository.UserExists(model.MacAddress))
			    {
				    ModelState.AddModelError(nameof(model.MacAddress), "The MAC Address provided is already registered with us.");
			    }
			    else
			    {
					await repository.CreateTableEntry(model);
					return RedirectToAction("RegistrationSuccessful");
				}
		    }

			return View(model);
	    }

		[Route("registration-successful")]
	    public IActionResult RegistrationSuccessful()
		{
			return View();
		}
    }
}
