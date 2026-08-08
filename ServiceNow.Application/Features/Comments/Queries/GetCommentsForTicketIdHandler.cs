using AutoMapper;
using MediatR;
using ServiceNow.ServiceNow.Application.DTOs;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;
namespace ServiceNow.ServiceNow.Application.Features.Comments.Queries

{
    public class GetCommentsForTicketIdQueryHandler
          : IRequestHandler<GetCommentsForTicketIdQuery, List<CreateCommentResponse>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentsForTicketIdQueryHandler(
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CreateCommentResponse>> Handle(
            GetCommentsForTicketIdQuery query,
            CancellationToken token)
        {
            var comments = await _commentRepository
                .GetCommentsByTicketId(query.TicketId);

            return _mapper.Map<List<CreateCommentResponse>>(comments);
        }
    }
}
