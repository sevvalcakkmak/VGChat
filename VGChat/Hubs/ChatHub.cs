using Microsoft.AspNetCore.SignalR;
using VGChat.Data;
using VGChat.Models;

namespace VGChat.Hubs
{
    namespace VGChat.Hubs
    {
        public class ChatHub : Hub
        {
            private readonly ApplicationDbContext _dbContext;

            public ChatHub(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task SendMessage(string user, string content)
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(content))
                {
                    // Hata durumunda veya null parametre gönderildiğinde yapılacak işlemler
                    return;
                }

                var message = new Message
                {
                    UserName = user,
                    Content = content,
                    MessageSentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };

                _dbContext.Messages.Add(message);
                await Clients.All.SendAsync("ReceiveMessage", message.UserName, message.Content, message.MessageSentTime);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
