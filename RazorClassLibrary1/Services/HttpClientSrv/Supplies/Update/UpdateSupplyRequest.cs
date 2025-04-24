namespace RazorClassLibrary1.Services.HttpClientSrv.Supplies.Update
{
    public record UpdateSupplyRequest(long Id, int Amount, double Price, string Description, long SupplyOperationId, long ServiceOrderTaskId, bool isActive);
}
