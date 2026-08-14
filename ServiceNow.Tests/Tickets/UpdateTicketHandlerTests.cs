using FluentAssertions;
using Moq;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Enum;
using Xunit;

namespace ServiceNow.Tests.Tickets;

public class UpdateTicketHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenTicketIsUpdatedSuccessfully()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();

        var command = new UpdateTicketCommand(
            1,
            "Updated Ticket",
            "Updated Description",
            TicketStatus.InProgress,
            Priority.high,
            2
        );

        repositoryMock
            .Setup(x => x.updateTicketInDb(command))
            .Returns(Task.FromResult(true));

        var handler = new UpdateTicketHandler(
            repositoryMock.Object
        );

        // Act

        var result = await handler.Handle(
            command,
            CancellationToken.None
        );

        // Assert

        result.Should().BeTrue();

        repositoryMock.Verify(
            x => x.updateTicketInDb(command),
            Times.Once
        );
    }


    [Fact]
    public async Task Handle_ShouldThrowException_WhenUpdateFails()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();

        var command = new UpdateTicketCommand(
            1,
            "Updated Ticket",
            "Updated Description",
            TicketStatus.InProgress,
            Priority.high,
            2
        );

        repositoryMock
            .Setup(x => x.updateTicketInDb(command))
            .ThrowsAsync(new Exception("Database update failed"));

        var handler = new UpdateTicketHandler(
            repositoryMock.Object
        );

        // Act

        var act = async () =>
            await handler.Handle(
                command,
                CancellationToken.None
            );

        // Assert

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Database update failed");

        repositoryMock.Verify(
            x => x.updateTicketInDb(command),
            Times.Once
        );
    }
}