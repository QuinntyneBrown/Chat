using System.Net;
using System.Threading.Tasks;
using Chat.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{messageId}", Name = "GetMessageByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMessageById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMessageById.Response>> GetById([FromRoute]GetMessageById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.Message == null)
            {
                return new NotFoundObjectResult(request.MessageId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetMessagesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMessages.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMessages.Response>> Get()
            => await _mediator.Send(new GetMessages.Request());
        
        [HttpPost(Name = "CreateMessageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateMessage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateMessage.Response>> Create([FromBody]CreateMessage.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetMessagesPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMessagesPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMessagesPage.Response>> Page([FromRoute]GetMessagesPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateMessageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateMessage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateMessage.Response>> Update([FromBody]UpdateMessage.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{messageId}", Name = "RemoveMessageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveMessage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveMessage.Response>> Remove([FromRoute]RemoveMessage.Request request)
            => await _mediator.Send(request);
        
    }
}
