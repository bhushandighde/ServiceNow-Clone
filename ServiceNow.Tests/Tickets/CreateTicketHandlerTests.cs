using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
using ServiceNow.ServiceNow.Application.Interfaces;
using TicketEntity = ServiceNow.ServiceNow.Domain.Entities.Tickets;
using Xunit;
using ServiceNow.ServiceNow.Domain.Enum;

namespace ServiceNow.Tests.Tickets;

public class CreateTicketHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenTicketIsCreatedSuccessfully()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();
        var mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<CreateTicketHandler>>();

        var command = new CreateTicketCommand(
      "Test Ticket",
      "Test Description",
      TicketStatus.Open,
      Priority.medium,
           1
            );

        var ticket = new TicketEntity
        {
            Title = "Test Ticket",
            Description = "Test Description",
            CreatedBy = 1
        };

        mapperMock
            .Setup(x => x.Map<TicketEntity>(command))
            .Returns(ticket);

        repositoryMock
            .Setup(x => x.AddTickettoDB(ticket))
            .ReturnsAsync(true);

        var handler = new CreateTicketHandler(
            repositoryMock.Object,
            mapperMock.Object,
            loggerMock.Object
        );

        // Act

        var result = await handler.Handle(
            command,
            CancellationToken.None
        );

        // Assert

        result.Should().BeTrue();

        mapperMock.Verify(
            x => x.Map<TicketEntity>(command),
            Times.Once
        );

        repositoryMock.Verify(
            x => x.AddTickettoDB(ticket),
            Times.Once
        );
    }


    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenTicketCreationFails()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();
        var mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<CreateTicketHandler>>();

        var command = new CreateTicketCommand(
         "Test Ticket",
         "Test Description",
         TicketStatus.Open,
         Priority.medium,
         1
     );

        var ticket = new TicketEntity
        {
            Title = "Test Ticket",
            Description = "Test Description",
            CreatedBy = 1
        };

        mapperMock
            .Setup(x => x.Map<TicketEntity>(command))
            .Returns(ticket);

        repositoryMock
            .Setup(x => x.AddTickettoDB(ticket))
            .ReturnsAsync(false);

        var handler = new CreateTicketHandler(
            repositoryMock.Object,
            mapperMock.Object,
            loggerMock.Object
        );

        // Act

        var result = await handler.Handle(
            command,
            CancellationToken.None
        );

        // Assert

        result.Should().BeFalse();

        mapperMock.Verify(
            x => x.Map<TicketEntity>(command),
            Times.Once
        );

        repositoryMock.Verify(
            x => x.AddTickettoDB(ticket),
            Times.Once
        );
    }
}