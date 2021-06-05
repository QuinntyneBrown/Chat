using System.Collections.Generic;

namespace Chat.Api.Core
{
    public class ResponseBase
    {
        public List<string> ValidationErrors { get; set; }
    }
}
