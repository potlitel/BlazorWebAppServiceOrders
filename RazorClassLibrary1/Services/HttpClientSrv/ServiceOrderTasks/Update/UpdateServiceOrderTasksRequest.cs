namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.Update
{
    public record UpdateServiceOrderTasksRequest(int Id, string? Observations, DateTime ExecutionDate, long ServiceOrderTaskStateId,
                                                 long ServiceOrderId, string CustomFieldSOTask, bool isActive);
}
