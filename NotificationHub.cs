using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace projectnew.hub
{
    public class NotificationHub : Hub
    {
        // Send message to all connected clients
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}

