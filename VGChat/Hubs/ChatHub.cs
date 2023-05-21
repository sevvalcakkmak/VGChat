﻿using Microsoft.AspNetCore.SignalR;
using VGChat.Data;
using VGChat.Models;

namespace VGChat.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendMessage(string user, string content)
        {
            var message = new Message
            {
                UserName = user,
                Content = content,
                MessageSentTime = DateTime.UtcNow
            };

            _dbContext.Messages.Add(message);
            await Clients.All.SendAsync("ReceiveMessage", message.UserName, message.Content, message.MessageSentTime);
            await _dbContext.SaveChangesAsync();
        }
    }
}
