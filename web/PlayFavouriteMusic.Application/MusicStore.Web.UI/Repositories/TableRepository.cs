using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MusicStore.Web.UI.Entities;
using MusicStore.Web.UI.ViewModels;

namespace MusicStore.Web.UI.Repositories
{
    public class TableRepository
    {
		const string CONN_STRING = @"DefaultEndpointsProtocol=https;AccountName=oshstorage;AccountKey=Xgj57VFZum5zRDy9czUA00lRQrYCzaemTVxgEzsvOPKIpPmdwreDuY0wUJnsln74TslXB54+sI9YmeK/X0EavQ==;EndpointSuffix=core.windows.net";
	    private const string TABLE_NAME = "PlayMusicRegistrations";
	    private const string PARTITION_KEY = "PlayMusicUsers";
	    private readonly CloudTable users;

	    public TableRepository()
	    {
			var acct = CloudStorageAccount.Parse(CONN_STRING);
			var tableClient = acct.CreateCloudTableClient();
			users = tableClient.GetTableReference(TABLE_NAME);
		}

		public async Task<TableResult> CreateTableEntry(RegistrationViewModel entry)
	    {
		    var musicUser = new PlayMusicUser(PARTITION_KEY, entry);
		    var insertOperation = TableOperation.Insert(musicUser);
		    return await users.ExecuteAsync(insertOperation);
	    }

	    public async Task<PlayMusicUser> GetUser(string macAddress)
	    {
		    var getOperation = TableOperation.Retrieve<PlayMusicUser>(PARTITION_KEY, macAddress);
		    var getResult = await users.ExecuteAsync(getOperation);

		    return getResult.Result as PlayMusicUser;
	    }

	    public async Task<List<PlayMusicUser>> GetAllUsers()
	    {
		    var getOperation = new TableQuery<PlayMusicUser>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, PARTITION_KEY));

		    var continuationToken = new TableContinuationToken();
			var queryResult = await users.ExecuteQuerySegmentedAsync(getOperation, continuationToken);

		    return queryResult.Results;
	    }

	    public async Task<bool> UserExists(string macAddress)
	    {
		    var user = await GetUser(macAddress);
		    return user != null;
	    }
	}
}
