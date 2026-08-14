using FluentAssertions;
using Moq;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.DeleteTicket;
using ServiceNow.ServiceNow.Application.Interfaces;
using Xunit;

namespace ServiceNow.Tests.Tickets;

public class DeleteTicketHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenTicketIsDeletedSuccessfully()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();

        var command = new DeleteTicketCommand(1);

        repositoryMock
            .Setup(x => x.DeleteTicketById(command.Id))
            .Returns(Task.FromResult(true));

        var handler = new DeleteTicketHandler(
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
            x => x.DeleteTicketById(command.Id),
            Times.Once
        );
    }


    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenRepositoryReturnsFalse()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();

        var command = new DeleteTicketCommand(1);

        repositoryMock
            .Setup(x => x.DeleteTicketById(command.Id))
            .Returns(Task.FromResult(false));

        var handler = new DeleteTicketHandler(
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
            x => x.DeleteTicketById(command.Id),
            Times.Once
        );
    }
}