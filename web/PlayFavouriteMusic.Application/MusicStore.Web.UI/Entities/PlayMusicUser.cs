using Microsoft.WindowsAzure.Storage.Table;
using MusicStore.Web.UI.ViewModels;

namespace MusicStore.Web.UI.Entities
{
    public class PlayMusicUser : TableEntity
    {
	    public PlayMusicUser()
	    {
		    
	    }

	    public PlayMusicUser(string partitionKey, RegistrationViewModel registration)
	    {
		    PartitionKey = partitionKey;
		    RowKey = registration.MacAddress;

		    Name = registration.Name;
		    MacAddress = registration.MacAddress;
		    Artist = registration.ArtistName;
		    Song = registration.FavouriteSong;
	    }

		public string Name { get; set; }
		public string MacAddress { get; set; }
		public string Artist { get; set; }
		public string Song { get; set; }
    }
}
