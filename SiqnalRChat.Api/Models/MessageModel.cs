
using System;

namespace SiqnalRChat.Api.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string UserName { get; set; }

        public string RoomCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool DeleteStatus { get; set; } = false;
    }
}
