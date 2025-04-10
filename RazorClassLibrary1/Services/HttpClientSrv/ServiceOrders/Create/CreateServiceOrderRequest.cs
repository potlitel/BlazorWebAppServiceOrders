using FluentValidation;
using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.Create
{
    public record CreateServiceOrderRequest(string Number, DateTime EstimatedEndingDate, string? Observations, string? Address,
                                            long OwnerId, long ExecutorId, long? ParentServiceOrderId, long ServiceOrderTypeId) 
                                            : IRequest<Result<CreateServiceOrderResponse>>;

    public class CreateServiceOrderRequestValidator : AbstractValidator<CreateServiceOrderRequest> {
        public CreateServiceOrderRequestValidator()
        {
            RuleFor(r => r.Number)
               .NotEmpty()
               .WithMessage("Please enter a Number")
               .Length(2, 50)
               .WithMessage("Number length should be between 2 and 50 characters");
            RuleFor(r => r.Observations)
                .NotEmpty()
                .WithMessage("Please enter a Observations")
                .Length(2, 255)
                .WithMessage("Observations length should be between 2 and 255 characters");
            RuleFor(r => r.Address)
               .NotEmpty()
               .WithMessage("Please enter a Address")
               .Length(2, 50)
               .WithMessage("Address length should be between 2 and 50 characters");
            RuleFor(r => r.OwnerId.ToString())
                .NotNull()
                .WithMessage("Please enter a OwnerId")
                .Length(2, 255)
                .WithMessage("OwnerId length should be between 2 and 255 characters");
            RuleFor(r => r.ExecutorId.ToString())
                .NotNull()
                .WithMessage("Please enter a ExecutorId")
                .Length(2, 255)
                .WithMessage("ExecutorId length should be between 2 and 255 characters");
            RuleFor(r => r.ParentServiceOrderId.ToString())
                .NotNull()
                .WithMessage("Please enter a ParentServiceOrderId")
                .Length(2, 255)
                .WithMessage("ParentServiceOrderId length should be between 2 and 255 characters");
            RuleFor(r => r.ServiceOrderTypeId.ToString())
                .NotNull()
                .WithMessage("Please enter a ServiceOrderTypeId")
                .Length(2, 255)
                .WithMessage("ServiceOrderTypeId length should be between 2 and 255 characters");
        }
    }
}
