using FluentValidation;

namespace ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketCommand>
    {

        public UpdateTicketValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");  

            RuleFor(x => x.Priority)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}
