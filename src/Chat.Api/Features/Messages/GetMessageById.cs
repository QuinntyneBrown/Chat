using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Chat.Api.Core;
using Chat.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Features
{
    public class GetMessageById
    {
        public class Request: IRequest<Response>
        {
            public Guid MessageId { get; set; }
        }

        public class Response: ResponseBase
        {
            public MessageDto Message { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IChatDbContext _context;
        
            public Handler(IChatDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Message = (await _context.Messages.SingleOrDefaultAsync(x => x.MessageId == request.MessageId)).ToDto()
                };
            }
            
        }
    }
}
