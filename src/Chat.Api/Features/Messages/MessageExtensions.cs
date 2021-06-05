using System;
using Chat.Api.Models;

namespace Chat.Api.Features
{
    public static class MessageExtensions
    {
        public static MessageDto ToDto(this Message message)
        {
            return new ()
            {
                MessageId = message.MessageId
            };
        }
        
    }
}
