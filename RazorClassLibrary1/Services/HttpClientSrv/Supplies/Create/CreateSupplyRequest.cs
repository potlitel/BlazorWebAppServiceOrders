namespace RazorClassLibrary1.Services.HttpClientSrv.Supplies.Create
{
    public record CreateSupplyRequest(int Amount, double Price, string Description, long SupplyOperationId, long ServiceOrderTaskId);
}
