using AutoMapper;
using FluentAssertions;
using Moq;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Tickets.Queries.GetTickets;
using ServiceNow.ServiceNow.Application.Interfaces;
using TicketEntity = ServiceNow.ServiceNow.Domain.Entities.Tickets;
using Xunit;

namespace ServiceNow.Tests.Tickets;

public class GetTicketsHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTickets_WhenTicketsExist()
    {
        // Arrange

        var tickets = new List<TicketEntity>
        {
            new TicketEntity
            {
                Id = 1,
                Title = "Ticket 1",
                Description = "Description 1"
            },
            new TicketEntity
            {
                Id = 2,
                Title = "Ticket 2",
                Description = "Description 2"
            }
        };

        var expectedResponse = new List<TicketResponseDto>
        {
            new TicketResponseDto
            {
                Id = 1,
                Title = "Ticket 1",
                Description = "Description 1"
            },
            new TicketResponseDto
            {
                Id = 2,
                Title = "Ticket 2",
                Description = "Description 2"
            }
        };

        var repositoryMock = new Mock<ITicketRespository>();

        repositoryMock
            .Setup(x => x.GetTicketsFromDB())
            .ReturnsAsync(tickets);

        var mapperMock = new Mock<IMapper>();

        mapperMock
            .Setup(x => x.Map<List<TicketResponseDto>>(tickets))
            .Returns(expectedResponse);

        var handler = new GetTicketsHandler(
            repositoryMock.Object,
            mapperMock.Object
        );

        var query = new GetTicketsQuery();

        // Act

        var result = await handler.Handle(
            query,
            CancellationToken.None
        );

        // Assert

        result.Should().NotBeNull();
        result.Should().HaveCount(2);

        result[0].Id.Should().Be(1);
        result[0].Title.Should().Be("Ticket 1");

        result[1].Id.Should().Be(2);
        result[1].Title.Should().Be("Ticket 2");

        repositoryMock.Verify(
            x => x.GetTicketsFromDB(),
            Times.Once
        );
    }


    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoTicketsExist()
    {
        // Arrange

        var tickets = new List<TicketEntity>();

        var expectedResponse = new List<TicketResponseDto>();

        var repositoryMock = new Mock<ITicketRespository>();

        repositoryMock
            .Setup(x => x.GetTicketsFromDB())
            .ReturnsAsync(tickets);

        var mapperMock = new Mock<IMapper>();

        mapperMock
            .Setup(x => x.Map<List<TicketResponseDto>>(tickets))
            .Returns(expectedResponse);

        var handler = new GetTicketsHandler(
            repositoryMock.Object,
            mapperMock.Object
        );

        var query = new GetTicketsQuery();

        // Act

        var result = await handler.Handle(
            query,
            CancellationToken.None
        );

        // Assert

        result.Should().NotBeNull();
        result.Should().BeEmpty();

        repositoryMock.Verify(
            x => x.GetTicketsFromDB(),
            Times.Once
        );
    }
}