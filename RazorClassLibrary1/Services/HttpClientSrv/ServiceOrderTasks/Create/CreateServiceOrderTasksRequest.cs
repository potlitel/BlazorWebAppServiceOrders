namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.Create
{
    public record CreateServiceOrderTasksRequest(string? Observations, DateTime ExecutionDate, long ServiceOrderTaskStateId,
                                                 long ServiceOrderId, string CustomFieldSOTask);
}
