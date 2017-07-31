namespace Lup.Software.Engineering
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
    using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

    // Create a table that will contain all the urlDrawers
    public class AzureTable
    {
        CloudStorageAccount storageAccount;

        static CloudTableClient tableClient;

        public AzureTable(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TableStorage");
            // Retrieve the storage account from the connection string.
            storageAccount = CloudStorageAccount.Parse(connectionString);
			tableClient = storageAccount.CreateCloudTableClient();
			CloudTable table = tableClient.GetTableReference("AzureTable");
			table.CreateIfNotExistsAsync(); // other option CreateNewTable(cloudTable);

			TableOperation retrieveOperation = TableOperation.Retrieve<UrlDrawer>("indice", "indice");
			TableResult retrievedResult = table.Execute(retrieveOperation);
            if (retrievedResult.Result == null)
			{
				UrlDrawer indice = new UrlDrawer("indice", "indice");
				InsertUrlDrawer(indice);
			}
		}

        // Get the indice (or number of element in the table)
        static public ulong GetIndice()
        {
            CloudTable table = tableClient.GetTableReference("AzureTable");
			TableOperation retrieveOperation = TableOperation.Retrieve<UrlDrawer>("indice", "indice");
			TableResult retrievedResult = table.Execute(retrieveOperation);
            return (retrievedResult.Result as UrlDrawer).Hits;
		}

        // Insert a new drawer in the table
		static public void InsertUrlDrawer(UrlDrawer urlDrawer)
		{
			CloudTable table = tableClient.GetTableReference("AzureTable");
			TableOperation insertOperation = TableOperation.Insert(urlDrawer);
			table.Execute(insertOperation);

			// Increment the indice
			TableOperation retrieveOperation = TableOperation.Retrieve<UrlDrawer>("indice", "indice");
			TableResult retrievedResult = table.Execute(retrieveOperation);

			UrlDrawer updateEntity = retrievedResult.Result as UrlDrawer;
			if (updateEntity != null)
			{
				updateEntity.Hits += 1;
				TableOperation updateOperation = TableOperation.Replace(updateEntity);
				table.Execute(updateOperation);
			}
		}

        // Get the shorten related to a Original Url
        static public string GetShorten(string originalUrl)
		{
			CloudTable table = tableClient.GetTableReference("AzureTable");

            var query = from entity in table.CreateQuery<UrlDrawer>()
            			where entity.OriginalUrl.Equals(originalUrl)
            			select entity;

            return (table.ExecuteQuery(query).Result as UrlDrawer).Shorten;
		}

        // Get the original Url from a shorten
		static public string GetOriginalUrl(string shortenUrl)
		{
			CloudTable table = tableClient.GetTableReference("AzureTable");

            var query = from entity in table.CreateQuery<UrlDrawer>()
            			where entity.Shorten.Equals(shortenUrl)
            			select entity;

            return (table.ExecuteQuery(query).Result as UrlDrawer).OriginalUrl;
		}

        // Get the number of hits recorded on this Url (only use for testing)
        static public ulong GetNumbeOfHit(string originalUrl)
		{
			CloudTable table = tableClient.GetTableReference("AzureTable");

			var query = from entity in table.CreateQuery<UrlDrawer>()
			          where entity.OriginalUrl.Equals(originalUrl)
			          select entity;

            return (table.ExecuteQuery(query).Result as UrlDrawer).Hits;
		}

        // Increment the number of hits on this Url related to this shorten
		static public void IncrementHit(string shortenUrl)
		{
			CloudTable table = tableClient.GetTableReference("AzureTable");

			var query = from entity2 in table.CreateQuery<UrlDrawer>()
						where entity2.Shorten.Equals(shortenUrl)
						select entity2;

			var entity = table.ExecuteQuery(query).Result as UrlDrawer;

            TableOperation updateOperation = TableOperation.Replace(entity);
			entity.Hits += 1;
			table.Execute(updateOperation);
		}
	}

	public interface ITableRepositories
	{
		void InsertUrlDrawer(UrlDrawer urlDrawer);

        List<UrlDrawer> GetUrlDrawers();

        string GetShortenUrl(string originalUrl);

        string GetOriginalUrl(string shortenUrl);
    }

    // Create a UrlDrawer entity that will store all data for a corresponding originalUrl
    public class UrlDrawer : TableEntity
	{
		public UrlDrawer()
		{
            this.OriginalUrl = string.Empty;
            this.Shorten = string.Empty;
            this.Hits = 0;
        }

        public UrlDrawer(string originalUrl, string shorten)
        {
            this.OriginalUrl = originalUrl;
            this.Shorten = shorten;
            this.Hits = 0;
            this.PartitionKey = originalUrl;
            this.RowKey = shorten;
        }

        public string OriginalUrl { get; set; }

        public string Shorten { get; set; }

        public ulong Hits { get; set; }
    }
}