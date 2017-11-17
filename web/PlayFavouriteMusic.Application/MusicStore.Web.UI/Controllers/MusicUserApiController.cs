using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.UI.Entities;
using MusicStore.Web.UI.Repositories;

namespace MusicStore.Web.UI.Controllers
{
	[Route("play-music/api")]
    public class MusicUserApiController : Controller
	{
		private readonly TableRepository repository;

		public MusicUserApiController(TableRepository repository)
		{
			this.repository = repository;
		}
		
		[HttpGet("users")]
		public async Task<IActionResult> Get()
		{
			var items = await repository.GetAllUsers();
			var result = items.Select(CreateNewUserDto);
			return Json(result);
		}

		[HttpGet("registered-mac-addresses")]
		public async Task<IActionResult> GetAllMacAddresses()
		{
			var users = await repository.GetAllUsers();
			var macAddresses = users.Select(u => u.MacAddress);
			return Json(macAddresses);
		}

		[HttpGet("users/{macAddress}")]
		public async Task<IActionResult> GetUser(string macAddress)
		{
			var user = await repository.GetUser(macAddress);

			if (user == null)
			{
				return NotFound();
			}

			return Json(CreateNewUserDto(user));
		}

		private dynamic CreateNewUserDto(PlayMusicUser user)
		{
			return new {user.Name, user.Artist, user.Song};
		}
    }
}
