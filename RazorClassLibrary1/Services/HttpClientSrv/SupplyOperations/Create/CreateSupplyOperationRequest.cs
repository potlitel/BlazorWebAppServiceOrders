using FluentValidation;
using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.Create
{
    public record CreateSupplyOperationRequest(string Code, string Description) : IRequest<Result<CreateSupplyOperationResponse>>;

    public class CreateSupplyOperationRequestValidator : AbstractValidator<CreateSupplyOperationRequest> {

        public CreateSupplyOperationRequestValidator()
        {
            RuleFor(r => r.Code)
              .NotEmpty()
              .WithMessage("Please enter a code")
              .Length(2, 50)
              .WithMessage("Code length should be between 2 and 50 characters");
            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Please enter a description")
                .Length(2, 255)
                .WithMessage("Description length should be between 2 and 255 characters");
        }

    }
}
