using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Base;
using Newtonsoft.Json;

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

        private static List<string> standByList;

        public static List<string> StandByList
        {
            get
            {
                if (standByList == null)
                    standByList = new List<string>();
                return standByList;
            }
        }

        private static readonly object ojb = new object();

        public async Task LogError(Exception ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("logErr", ex.Message);
        }

        public async Task DuelStart(string id1, string id2)
        {
            Game game = new Game(id1, id2);
            string s = JsonConvert.SerializeObject(game);
            await Clients.Clients(new string[] { id1, id2 }).SendAsync("duelInit", s);
        }

        public async Task OnlineNumbers()
        {
            await Clients.All.SendAsync("onlineNums", ClientIDs.Count);
        }

        public async Task StandBy()
        {
            string id = Context.ConnectionId;
            if (!ClientIDs.Remove(id))
                throw new Exception("准备异常！");
            StandByList.Add(id);
            if (StandByList.Count > 1)
            {
                string id1 = StandByList.Last();
                StandByList.RemoveAt(StandByList.Count - 1);
                string id2 = StandByList.Last();
                StandByList.RemoveAt(StandByList.Count - 1);
                try
                {
                    await DuelStart(id1, id2);
                }
                catch (Exception ex)
                {
                    await LogError(ex);
                }
            }
            await OnlineNumbers();
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
