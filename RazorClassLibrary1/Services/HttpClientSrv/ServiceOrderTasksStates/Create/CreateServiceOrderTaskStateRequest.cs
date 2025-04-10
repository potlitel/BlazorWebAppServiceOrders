using FluentValidation;
using FSA.Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.Create
{
    public record CreateServiceOrderTaskStateRequest(string Code, string Description) : IRequest<Result<CreateServiceOrderTaskStateResponse>>;

    public class CreateServiceOrderTaskRequestValidator : AbstractValidator<CreateServiceOrderTaskStateRequest> {
        public CreateServiceOrderTaskRequestValidator()
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
