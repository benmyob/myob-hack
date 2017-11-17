using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Web.UI.ViewModels
{
    public class RegistrationViewModel
    {
		[Required(ErrorMessage = "Your name is required.")]
		[DisplayName("Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "MAC Address is required.")]
		[DisplayName("MAC Address")]
		public string MacAddress { get; set; }

		[Required(ErrorMessage = "Artist Name is required.")]
		[DisplayName("Artist")]
		public string ArtistName { get; set; }

		[Required(ErrorMessage = "Song name is required.")]
		[DisplayName("Song")]
		public string FavouriteSong { get; set; }
    }
}
