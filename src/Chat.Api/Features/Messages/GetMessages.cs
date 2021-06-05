using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Chat.Api.Core;
using Chat.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Features
{
    public class GetMessages
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<MessageDto> Messages { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IChatDbContext _context;
        
            public Handler(IChatDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Messages = await _context.Messages.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
