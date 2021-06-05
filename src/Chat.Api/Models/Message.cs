using System;

namespace Chat.Api.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Body {get; private set; }

        public Message(string body)
        {
            Body = body;
        }

        private Message()
        {

        }
    }
}
