using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Chat.Api.Models;
using Chat.Api.Core;
using Chat.Api.Interfaces;

namespace Chat.Api.Features
{
    public class RemoveMessage
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
                var message = await _context.Messages.SingleAsync(x => x.MessageId == request.MessageId);
                
                _context.Messages.Remove(message);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Message = message.ToDto()
                };
            }
            
        }
    }
}
