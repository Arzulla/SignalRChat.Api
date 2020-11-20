using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiqnalRChat.Api.Infrastructure.SiqnalR
{
    public class SendMessageModel
    {
        public string Message { get; set; }

        public string UserName { get; set; }

        public string RoomCode { get; set; }
    }
}
