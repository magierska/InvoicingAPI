namespace InvoicingAPI.CosmosDb;

internal static class Consts
{
    public static string PartitionKeyPath = "/customerId";

    public static class StoredProcedureNames
    {
        public static string CreateInvoice = "createInvoice";
    }
}
