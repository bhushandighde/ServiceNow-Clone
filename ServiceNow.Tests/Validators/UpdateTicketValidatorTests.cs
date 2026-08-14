using FluentAssertions;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket;
using ServiceNow.ServiceNow.Domain.Enum;
using Xunit;

namespace ServiceNow.Tests.Validators
{
    public class UpdateTicketValidatorTests
    {
        private readonly UpdateTicketValidator _validator = new();

        [Fact]
        public void Should_Fail_When_Title_Is_Empty()
        {
            var command = new UpdateTicketCommand(
                1,
                "",
                "Test description",
                TicketStatus.Open,
                Priority.low,
                null);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_Description_Is_Empty()
        {
            var command = new UpdateTicketCommand(
                1,
                "Test Ticket",
                "",
                TicketStatus.Open,
                Priority.low,
                null);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new UpdateTicketCommand(
                1,
                "Updated Ticket",
                "Updated description",
                TicketStatus.Open,
                Priority.low,
                null);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}