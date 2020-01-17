using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Base;

namespace YuGiOh2.Hubs
{
    public class DuelHub : Hub
    {
        private static HashSet<string> clientIDs;

        public static HashSet<string> ClientIDs
        {
            get
            {
                if (clientIDs == null)
                    clientIDs = new HashSet<string>();
                return clientIDs;
            }
        }
        public async Task DuelStart()
        {
            Game game = new Game();
            await Clients.All.SendAsync("DuelInit", game);
        }

        public async Task OnlineNumbers()
        {
            await Clients.All.SendAsync("onlineNums", ClientIDs.Count);
        }

        public override Task OnConnectedAsync()
        {
            ClientIDs.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ClientIDs.Remove(Context.ConnectionId);
            await OnlineNumbers();
        }
    }
}
