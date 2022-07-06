using InvoicingAPI.CosmosDb.Exceptions;
using InvoicingAPI.CosmosDb.Models;
using InvoicingAPI.CosmosDb.Models.Invoices;
using InvoicingAPI.Domain.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvoicingAPI.CosmosDb.Repositories
{
    public partial class InvoicingDbRepository : IInvoicingDbRepository
    {
        public async Task<Domain.Entities.Invoices.Invoice> GetInvoiceAsync(Guid invoiceId, Guid customerId, CancellationToken cancellationToken = default)
        {
            var iterator = container
                .GetItemLinqQueryable<Invoice>()
                .Where(x => x.Id == invoiceId && x.CustomerId == customerId && x.EntityType == EntityType.Invoice)
                .ToFeedIterator();
            if (iterator.HasMoreResults)
            {
                var results = await iterator.ReadNextAsync();
                var invoice = results?.SingleOrDefault();
                if (invoice != null)
                {
                    return mapper.Map<Domain.Entities.Invoices.Invoice>(invoice);
                }
            }

            logger.LogError("Retrieving invoice failed. Invoice with id: '{InvoiceId}' and partition key: '{PartitionKey}' was not found in the container: '{ContainerId}'.", invoiceId, customerId, container.Id);
            throw new CosmosRecordNotFoundException(invoiceId, customerId, container);
        }

        public async Task CreateInvoiceAsync(Domain.Entities.Invoices.Invoice invoice, CancellationToken cancellationToken = default)
        {
            var item = mapper.Map<Invoice>(invoice);

            var customerId = invoice.CustomerId.ToString();
            var partitionKey = new PartitionKey(customerId);

            var success = await container.Scripts.ExecuteStoredProcedureAsync<bool>(Consts.StoredProcedureNames.CreateInvoice, partitionKey, new dynamic[] { item });
            if (!success)
            {
                logger.LogError("Creating invoice failed. Customer with id: '{CustomerId}' and partition key: '{PartitionKey}' was not found in the container: '{ContainerId}'.", customerId, partitionKey, container.Id);
                throw new CosmosRecordNotFoundException(invoice.CustomerId, invoice.CustomerId, container);
            }
        }

        public async Task UpdateStateAsync(Guid invoiceId, Guid customerId, Domain.Entities.Invoices.InvoiceState state, CancellationToken cancellationToken = default)
        {
            var id = invoiceId.ToString();
            var partitionKey = new PartitionKey(customerId.ToString());
            try
            {
                await container.PatchItemAsync<Invoice>(id, partitionKey, new[] { PatchOperation.Replace("/state", state) });
            }
            catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                logger.LogError(e, "Updating state failed. Invoice with id: '{InvoiceId}' and partition key: '{PartitionKey}' was not found in the container: '{ContainerId}'.", id, partitionKey, container.Id);
                throw new CosmosRecordNotFoundException(invoiceId, customerId, container, e);
            }
        }
    }
}
