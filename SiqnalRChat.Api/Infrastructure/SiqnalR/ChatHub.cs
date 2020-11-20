using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SiqnalRChat.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiqnalRChat.Api.Infrastructure.SiqnalR
{
    public class ChatHub : Hub
    {
        private readonly IChatRepository _chatRepository;
        public ChatHub(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task AddToGroup(string roomCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task SendMessage(SendMessageModel message)
        {
            ReceiveMessageModel response = null;

            var task = Task.Run(() =>
            {
                MessageModel msg = new MessageModel
                {
                    UserName = message.UserName,
                    Message = message.Message,
                    RoomCode = message.RoomCode,
                    CreatedDate = DateTime.Now
                };

                MessageModel responseMsg = _chatRepository.Add(msg);

                response = new ReceiveMessageModel
                {
                    UserName = responseMsg.UserName,
                    Message = responseMsg.Message,
                    RoomCode = responseMsg.RoomCode,
                    CreateDate = responseMsg.CreatedDate
                };
            });

            task.Wait();

            if (response != null)
            {
                await Clients.Group(message.RoomCode).SendAsync("ReceiveMessage", response);
            }
        }

        public async Task JoinGroup(string roomCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }
        public async Task LeaveGroup(string roomCode)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomCode);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Connected", Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("DisConnected", Context.ConnectionId);
        }


    }
}
