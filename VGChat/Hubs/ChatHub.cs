using Microsoft.AspNetCore.SignalR;
using VGChat_demo.Data;
using VGChat_demo.Models;

namespace VGChat_demo.Hubs { 


public class ChatHub : Hub
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
            userName = user,
            Content = content,
            MessageSentTime = DateTime.UtcNow
        };

        _dbContext.Messages.Add(message);
        await Clients.All.SendAsync("ReceiveMessage", message.userName, message.Content, message.MessageSentTime);
        await _dbContext.SaveChangesAsync();

       
    }
}
}