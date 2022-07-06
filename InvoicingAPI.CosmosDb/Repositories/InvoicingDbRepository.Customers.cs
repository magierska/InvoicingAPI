using AutoMapper;
using InvoicingAPI.CosmosDb.Exceptions;
using InvoicingAPI.CosmosDb.Models;
using InvoicingAPI.CosmosDb.Models.Customers;
using InvoicingAPI.Domain.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace InvoicingAPI.CosmosDb.Repositories
{
    public partial class InvoicingDbRepository : IInvoicingDbRepository
    {
        private readonly ILogger<InvoicingDbRepository> logger;
        private readonly IMapper mapper;
        private readonly Container container;

        public InvoicingDbRepository(ILogger<InvoicingDbRepository> logger, IMapper mapper, CosmosClient client, IOptions<DbConfiguration> options)
        {
            this.logger = logger;
            this.mapper = mapper;
            container = client.GetContainer(options.Value.DbName, options.Value.ContainerName);
        }

        public async Task AddCustomerAsync(Domain.Entities.Customers.Customer customer, CancellationToken cancellationToken = default)
        {
            var item = mapper.Map<Customer>(customer);
            await container.CreateItemAsync(item);
        }

        public async Task ReplaceContactDetailsAsync(Guid customerId, IEnumerable<Domain.Entities.Customers.ContactDetail> contactDetails, CancellationToken cancellationToken = default)
        {
            var id = customerId.ToString();
            var partitionKey = new PartitionKey(id);
            try
            {
                await container.PatchItemAsync<Domain.Entities.Customers.ContactDetail>(id, partitionKey, new[] { PatchOperation.Replace("/contactDetails", contactDetails) });
            }
            catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                logger.LogError(e, "Replacing contact details failed. Record with id: '{CustomerId}' and partition key: '{PartitionKey}' was not found in the container: '{ContainerId}'.", id, partitionKey, container.Id);
                throw new CosmosRecordNotFoundException(customerId, customerId, container, e);
            }
        }
    }
}
