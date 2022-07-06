namespace InvoicingAPI.CosmosDb.Models;

public class DbConfiguration
{
    public string? EndpointUri { get; set; }

    public string? PrimaryKey { get; set; }

    public string? DbName { get; set; }

    public string? ContainerName { get; set; }
}
