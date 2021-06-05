using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Chat.Api.Models;
using Chat.Api.Core;
using Chat.Api.Interfaces;

namespace Chat.Api.Features
{
    public class CreateMessage
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Message).NotNull();
                RuleFor(request => request.Message).SetValidator(new MessageValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public MessageDto Message { get; set; }
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
                var message = new Message();
                
                _context.Messages.Add(message);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Message = message.ToDto()
                };
            }
            
        }
    }
}
