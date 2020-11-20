using System;

namespace SiqnalRChat.Api.Infrastructure.SiqnalR
{
    public class ReceiveMessageModel
    {
        public string UserName { get; set; }

        public string Message { get; set; }

        public string RoomCode { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
