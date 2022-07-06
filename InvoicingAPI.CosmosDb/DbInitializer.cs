using InvoicingAPI.CosmosDb.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Microsoft.Extensions.Options;
using System.Net;

namespace InvoicingAPI.CosmosDb
{
    public class DbInitializer
    {
        private readonly CosmosClient client;
        private readonly DbConfiguration dbConfiguration;

        public DbInitializer(CosmosClient client, IOptions<DbConfiguration> dbConfigurationOptions)
        {
            this.client = client;
            dbConfiguration = dbConfigurationOptions.Value;
        }

        public async Task InitAsync()
        {
            var databaseResponse = await client.CreateDatabaseIfNotExistsAsync(dbConfiguration.DbName);
            var containerResponse = await databaseResponse.Database.CreateContainerIfNotExistsAsync(dbConfiguration.ContainerName, Consts.PartitionKeyPath);
            await UpsertStoredProcedureAsync(containerResponse.Container, Consts.StoredProcedureNames.CreateInvoice);
        }

        private async Task<StoredProcedureResponse> UpsertStoredProcedureAsync(Container container, string name)
        {
            var sp = new StoredProcedureProperties
            {
                Id = name,
                Body = File.ReadAllText($@"../InvoicingAPI.CosmosDb/StoredProcedures/{name}.js")
            };

            try
            {
                return await container.Scripts.ReplaceStoredProcedureAsync(sp);
            }
            catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return await container.Scripts.CreateStoredProcedureAsync(sp);
            }
        }
    }
}
