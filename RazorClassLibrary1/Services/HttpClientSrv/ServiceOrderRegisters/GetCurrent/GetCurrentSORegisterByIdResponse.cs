using FSA.Core.ServiceOrders.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetCurrent
{
    //public record GetCurrentSORegisterByIdResponse(ServiceOrderRegisterDto ServiceOrderRegisterDto);

    public class Data
    {
        public string Trigger { get; set; }
        public string StateFrom { get; set; }
        public string StateTo { get; set; }
        public string Observations { get; set; }
        public int ServiceOrderId { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    //https://json2csharp.com/
    public class Root
    {
        public Data Data { get; set; }
        public bool Succeeded { get; set; }
        public List<object> Errors { get; set; }
        public int StatusCode { get; set; }
        public object Pagination { get; set; }
    }



    public record GetCurrentSORegisterByIdResponse();
}
