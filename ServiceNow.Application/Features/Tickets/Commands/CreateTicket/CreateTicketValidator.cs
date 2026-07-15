using FluentValidation;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(x => x.CreatedBy)
                .GreaterThan(0)
                .WithMessage("Invalid user.");

            RuleFor(x => x.Priority)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}