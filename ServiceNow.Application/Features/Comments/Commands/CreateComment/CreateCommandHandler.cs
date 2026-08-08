using AutoMapper;
using Azure.Core;
using MediatR;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Application.DTOs;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using ServiceNow.ServiceNow.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
namespace ServiceNow.ServiceNow.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentResponse>
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;
        public CreateCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
           _commentRepository = commentRepository;
            _mapper = mapper;

        }

        public async Task<CreateCommentResponse>  Handle(CreateCommentCommand command, CancellationToken token)
        {
            var comment = _mapper.Map<ServiceNow.Domain.Entities.Comment>(command);

            var response = await _commentRepository.AddComment(comment);

            return new CreateCommentResponse
            {
                Id = comment.Id,
                TicketId = comment.TicketId,
                Text = comment.Text,
                UserId = comment.CreatedBy,
                CreatedAt = comment.CreatedAt

            };
        }
    }
}
