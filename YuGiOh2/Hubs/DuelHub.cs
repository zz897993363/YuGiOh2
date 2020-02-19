using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Base;
using Newtonsoft.Json;
using System.Threading;

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

        private static Dictionary<string, Game> games;

        public static Dictionary<string, Game> Games
        {
            get
            {
                if (games == null)
                    games = new Dictionary<string, Game>();
                return games;
            }
        }

        private static readonly object obj = new object();

        public async Task LogError(Exception ex)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("logErr", ex.Message + ex.StackTrace);
        }

        //初始化对局
        public async Task DuelStart(string id1, string id2)
        {
            Game game = new Game(id1, id2);
            Games.Add(game.UID, game);
            game.Player1.DrawPhase();
            game.Player2.DrawPhase();
            game.Player2.YourTurn = false;
            var msg1 = MessageFactory.GetGameMessage(game.Player1, game.Player2, game.UID);
            var msg2 = MessageFactory.GetGameMessage(game.Player2, game.Player1, game.UID);
            string msgStr1 = JsonConvert.SerializeObject(msg1);
            string msgStr2 = JsonConvert.SerializeObject(msg2);
            await Clients.Clients(id1).SendAsync("duelInit", msgStr1);
            await Clients.Clients(id2).SendAsync("duelInit", msgStr2);
        }

        public void InitComplete(string uid)
        {
            lock (obj)
            {
                Games[uid].InitCount++;
                if (Games[uid].InitCount > 1)
                {
                    TurnStart(uid, Games[uid].Player1.ID);
                }
            }
        }

        public async Task TurnStart(string uid, string playerID)
        {
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;
            Player1.DrawPhase();
            Player1.FirstTurn = true;
            await SendMessage(Player1, Player2, uid);
        }

        public async Task SendMessage(Player player1, Player player2, string uid)
        {
            var msg1 = MessageFactory.GetGameMessage(player1, player2, uid);
            var msg2 = MessageFactory.GetGameMessage(player2, player1, uid);
            string msgStr1 = JsonConvert.SerializeObject(msg1);
            string msgStr2 = JsonConvert.SerializeObject(msg2);
            player1.Message = "";
            player2.Message = "";
            await Clients.Clients(player1.ID).SendAsync("renderGame", msgStr1);
            await Clients.Clients(player2.ID).SendAsync("renderGame", msgStr2);
        }

        public async Task SummonFromHands(string uid, string cardID)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;
            if (!Player1.CanSummon)
                return;

            Player1.SummonMonsterFromHands(cardID);
            Player1.ProcessContinuousEffectWhenSummon(cardID);
            Player2.ProcessContinuousEffectWhenSummon(cardID);
            Player1.CheckTrapsWhenSummon();
            if (Player2.EffectingCard != null)
            {
                await SendMessage(Player1, Player2, uid);
                Thread.Sleep(500);
                Player2.ProcessEffect(cardID);
            }

            await SendMessage(Player1, Player2, uid);
        }

        public async Task EffectFromHands(string uid, string cardID)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;

            Player1.EffectFromHands(cardID);

            if (Player1.ChooseTarget == 0)
            {
                await SendMessage(Player1, Player2, uid);
                Thread.Sleep(1000);
                Player1.ProcessEffect(null);
            }

            await SendMessage(Player1, Player2, uid);
        }

        public async Task EffectFromField(string uid, int index)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;

            Player1.EffectFromField(index);

            if (Player1.ChooseTarget == 0)
            {
                await SendMessage(Player1, Player2, uid);
                Thread.Sleep(1000);
                Player1.ProcessEffect(null);
            }

            await SendMessage(Player1, Player2, uid);
        }

        public async Task ProcessEffect(string uid, string targetID)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;

            Player1.ProcessEffect(targetID);

            await SendMessage(Player1, Player2, uid);
        }

        public async Task SetFromHands(string uid, string cardID)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;

            Player1.SetFromHands(cardID);

            await SendMessage(Player1, Player2, uid);
        }

        public async Task DirectAttack(string uid, int index)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;

            MonsterCard card = Player1.Field.MonsterFields[index];
            if (card == null)
                return;
            Player1.ProcessContinuousEffectWhenAttack(card.UID);
            Player2.ProcessContinuousEffectWhenAttack(card.UID);
            Player1.CheckTrapsWhenAttack();
            if (Player2.EffectingCard != null)
            {
                await SendMessage(Player1, Player2, uid);
                Thread.Sleep(500);
                Player2.ProcessEffect(card.UID);
            }
            Player1.DirectAttack(index);

            await SendMessage(Player1, Player2, uid);
        }

        public async Task Battle(string uid, int index1, int index2)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];

            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;
            try
            {
                MonsterCard card1 = Player1.Field.MonsterFields[index1];
                MonsterCard card2 = Player2.Field.MonsterFields[index2];
                if (card1 == null)
                    return;
                if (card2 == null)
                {
                    await DirectAttack(uid, index1);
                    return;
                }
                Player1.ProcessContinuousEffectWhenAttack(card1.UID);
                Player2.ProcessContinuousEffectWhenAttack(card1.UID);
                Player1.CheckTrapsWhenAttack();
                if (Player2.EffectingCard != null)
                {
                    await SendMessage(Player1, Player2, uid);
                    Thread.Sleep(500);
                    Player2.ProcessEffect(card1.UID);
                }
                Player1.Battle(index1, index2);
            }
            catch (Exception ex)
            {
                await LogError(ex);
            }

            await SendMessage(Player1, Player2, uid);
        }

        public async Task ChangePosition(string uid, int index)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;

            MonsterCard card = Player1.Field.MonsterFields[index];
            if (card == null)
                return;

            Player1.ChangePosition(card);

            await SendMessage(Player1, Player2, uid);
        }

        public async Task EndPhase(string uid)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;
            Player1.EndPhase();
            Player2.DrawPhase();
            Player2.StandByPhase();
            await SendMessage(Player1, Player2, uid);
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
