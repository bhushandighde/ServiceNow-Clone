using FluentAssertions;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
using ServiceNow.ServiceNow.Domain.Enum;
using Xunit;

namespace ServiceNow.ServiceNow.Tests.Validators
{
    public class CreateTicketValidatorTests
    {
        private readonly CreateTicketValidator _validator = new();

        [Fact]
        public void Should_Fail_When_Title_Is_Empty()
        {
            var command = new CreateTicketCommand(
                "",
                "Test description",
                TicketStatus.Open,
                Priority.low,
                1);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_Description_Is_Empty()
        {
            var command = new CreateTicketCommand(
                "Test Ticket",
                "",
                TicketStatus.Open,
                Priority.low,
                1);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_CreatedBy_Is_Invalid()
        {
            var command = new CreateTicketCommand(
                "Test Ticket",
                "Test description",
                TicketStatus.Open,
                Priority.low,
                0);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new CreateTicketCommand(
                "Test Ticket",
                "Test description",
                TicketStatus.Open,
                Priority.low,
                1);

            var result = _validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}