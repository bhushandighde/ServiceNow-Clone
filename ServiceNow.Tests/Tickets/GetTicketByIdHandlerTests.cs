using AutoMapper;
using FluentAssertions;
using Moq;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTicketById;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;
using ServiceNow.ServiceNow.Domain.Enum;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServiceNow.ServiceNow.Tests.Tickets;

public class GetTicketByIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTicket_WhenTicketExists()
    {
        // Arrange
        var ticket = new Domain.Entities.Tickets
        {
            Id = 1,
            Title = "Test Ticket",
            Description = "Test Description",
            Status = Domain.Enum.TicketStatus.Open,
            Priority = Domain.Enum.Priority.low,
            CreatedBy = 1,
            AssignedTo = 1
        };

        var repositoryMock = new Mock<ITicketRespository>();

        repositoryMock
            .Setup(x => x.GetTicketsByIdFromDB(1))
            .ReturnsAsync(ticket);
        var mapperMock = new Mock<IMapper>();

        var expectedResponse = new TicketResponseDto
        {
            Id = 1,
            Title = "Test Ticket",
            Description = "Test Description",
            Status = TicketStatus.Open,
            Priority = Domain.Enum.Priority.low,
            CreatedBy = 1,
            AssignedTo = 1
        };

        mapperMock
            .Setup(x => x.Map<TicketResponseDto>(ticket))
            .Returns(expectedResponse);

        var handler = new GetTicketsByIdHandler(
                   repositoryMock.Object,
                   mapperMock.Object
               );


        // Act

        var query = new GetTicketsByIdQuery
        {
            TicketId = 1
        };

        var result = await handler.Handle(
            query,
            CancellationToken.None
        );

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Title.Should().Be("Test Ticket");
        result.Description.Should().Be("Test Description");
        result.Status.Should().Be(Domain.Enum.TicketStatus.Open);
        result.Priority.Should().Be(Domain.Enum.Priority.low);
    }



    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenTicketDoesNotExist()
    {
        // Arrange

        var repositoryMock = new Mock<ITicketRespository>();

        repositoryMock
            .Setup(x => x.GetTicketsByIdFromDB(999))
            .ReturnsAsync((Domain.Entities.Tickets?)null);

        var mapperMock = new Mock<IMapper>();

        var handler = new GetTicketsByIdHandler(
            repositoryMock.Object,
            mapperMock.Object
        );

        var query = new GetTicketsByIdQuery
        {
            TicketId = 999
        };

        // Act

        Func<Task> act = async () =>
            await handler.Handle(
                query,
                CancellationToken.None
            );

        // Assert

        await act.Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage("Ticket 999 not found.");

        mapperMock.Verify(
            x => x.Map<TicketResponseDto>(
                It.IsAny<Domain.Entities.Tickets>()
            ),
            Times.Never
        );
    }
}