using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Chat.Api.Extensions;
using Chat.Api.Core;
using Chat.Api.Interfaces;
using Chat.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Features
{
    public class GetMessagesPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<MessageDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IChatDbContext _context;
        
            public Handler(IChatDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from message in _context.Messages
                    select message;
                
                var length = await _context.Messages.CountAsync();
                
                var messages = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = messages
                };
            }
            
        }
    }
}
