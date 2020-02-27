using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Base;
using Newtonsoft.Json;
using System.Threading;
using YuGiOh2.Data;

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

        private static List<string> standByIDs;
        public static List<string> StandByIDs
        {
            get
            {
                if (standByIDs == null)
                    standByIDs = new List<string>();
                return standByIDs;
            }
        }

        private static Dictionary<string, string> userConnectionIDs;
        public static Dictionary<string, string> UserConnectionIDs
        {
            get
            {
                if (userConnectionIDs == null)
                    userConnectionIDs = new Dictionary<string, string>();
                return userConnectionIDs;
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

        private static Dictionary<string, int> decks;
        public static Dictionary<string, int> Decks
        {
            get
            {
                if (decks == null)
                    decks = new Dictionary<string, int>();
                return decks;
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
            if (Decks.ContainsKey(id1) && Decks[id1] > 0 && Decks[id1] <= DuelUtils.GetAllDecks().Count)
            {
                game.Player1.Deck = DuelUtils.GetDeck(Decks[id1]);
            }
            if (Decks.ContainsKey(id2) && Decks[id2] > 0 && Decks[id2] <= DuelUtils.GetAllDecks().Count)
            {
                game.Player2.Deck = DuelUtils.GetDeck(Decks[id2]);
            }
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
            if (player1.Lose || player2.Lose)
            {
                Games.Remove(uid);
                Context.Abort();
            }
        }

        public async Task SendMessageToPlayer1(Player player1, Player player2, string uid)
        {
            var msg1 = MessageFactory.GetGameMessage(player1, player2, uid);
            string msgStr1 = JsonConvert.SerializeObject(msg1);
            player1.Message = "";
            await Clients.Clients(player1.ID).SendAsync("renderGame", msgStr1);
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
            if (Player1.Hands.Any(c => c.UID == cardID))
                return;

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
            if (Player1.Hands.Any(c => c.UID == cardID))
                return;

            if (Player1.ChooseTarget <= 0)
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

            if (Player1.ChooseTarget <= 0)
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
            await Clients.All.SendAsync("onlineNums", ClientIDs.Count + StandByIDs.Count + Games.Count * 2);
        }

        public async Task StandBy(int deckIndex)
        {
            string id = Context.ConnectionId;
            if (!ClientIDs.Remove(id))
                throw new Exception("准备异常！");
            StandByIDs.Add(id);
            Decks.Add(id, deckIndex);
            if (StandByIDs.Count > 1)
            {
                string id1 = StandByIDs.First();
                StandByIDs.Remove(id1);
                string id2 = StandByIDs.First();
                StandByIDs.Remove(id2);
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

        public async void Chat(string uid, string message)
        {
            string playerID = Context.ConnectionId;
            Game game = Games[uid];
            Player Player1 = game.Player1.ID == playerID ? game.Player1 : game.Player2;
            Player Player2 = game.Player1.ID == playerID ? game.Player2 : game.Player1;
            await Clients.Client(Player1.ID).SendAsync("updateChatroom", "你：" + message);
            await Clients.Client(Player2.ID).SendAsync("updateChatroom", "对方：" + message);
        }

        public async void UserIdentification(string cookie)
        {
            if (String.IsNullOrEmpty(cookie))
            {
                cookie = Guid.NewGuid().ToString();
            }

            string newID = Context.ConnectionId;
            bool modifiedInGame = false;
            if (UserConnectionIDs.TryGetValue(cookie, out string oldID))
            {
                foreach (var game in Games.Values)
                {
                    if (game.Player1.ID == oldID)
                    {
                        game.Player1.ID = newID;
                        modifiedInGame = true;
                        await SendMessageToPlayer1(game.Player1, game.Player2, game.UID);
                    }
                    else if (game.Player2.ID == oldID)
                    {
                        game.Player2.ID = newID;
                        modifiedInGame = true;
                        await SendMessageToPlayer1(game.Player2, game.Player1, game.UID);
                    }
                    if (modifiedInGame)
                        break;
                }
            }
            if (!modifiedInGame)
            {
                if (oldID != null)
                {
                    StandByIDs.Remove(oldID);
                    Decks.Remove(oldID);
                    ClientIDs.Remove(oldID);
                }
                ClientIDs.Add(newID);
            }
            UserConnectionIDs[cookie] = newID;
            await Clients.Clients(newID).SendAsync("setCookie", cookie);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ClientIDs.Remove(Context.ConnectionId);
            StandByIDs.Remove(Context.ConnectionId);
            Decks.Remove(Context.ConnectionId);
            await OnlineNumbers();
        }
    }
}
