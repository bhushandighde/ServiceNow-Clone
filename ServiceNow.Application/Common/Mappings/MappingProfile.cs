using AutoMapper;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Features.Comments.Commands.CreateComment;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.CreateTicket;
using ServiceNow.ServiceNow.Application.Features.Tickets.Commands.UpdateTicket;
using ServiceNow.ServiceNow.Domain.Entities;

namespace ServiceNow.ServiceNow.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tickets, TicketResponseDto>();
            CreateMap<CreateTicketCommand, Tickets>();
            CreateMap<UpdateTicketCommand, Tickets>();
            CreateMap<CreateCommentCommand, Comment>();
            CreateMap<Comment, CreateCommentResponse>();

        }
    }
}