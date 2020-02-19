using System;
using System.Linq;
using YuGiOh2.Data;

namespace YuGiOh2.Base
{
    public class Game
    {
        public int InitCount;
        public readonly string UID;
        public Player Player1
        {
            get => Players[0];
            set => Players[0] = value;
        }
        public Player Player2
        {
            get => Players[1];
            set => Players[1] = value;
        }
        public readonly PlayerCollection Players;

        public Game(string id1, string id2)
        {
            Players = new PlayerCollection();
            UID = Guid.NewGuid().ToString();
            Player1 = new Player(null, id1);
            Player2 = new Player(null, id2);
            Player1.Enemy = Player2;
            Player2.Enemy = Player1;
            BuildDeck(Player1);
            BuildDeck(Player2);
        }

        private static void BuildDeck(Player player)
        {
            var cardBase = DuelUtils.GetAllCards();
            var monsters = cardBase.Where(c => c.Category == (int)CardCategory.Monster).ToList();
            var spellAndTraps = cardBase.Where(c => c.Category != (int)CardCategory.Monster).ToList();
            int[] check = new int[monsters.Count + spellAndTraps.Count];
            Random rd = new Random();
            player.Deck.Add(new SpellAndTrapCard(spellAndTraps.FirstOrDefault(c => c.Password == "83764718")));
            
            for (int i = 0; i < 20; i++)
            {
                int idx = rd.Next(0, monsters.Count);
                while (check[idx] >= 3)
                {
                    idx = rd.Next(0, monsters.Count);
                }
                player.Deck.Insert(rd.Next(player.Deck.Count), new MonsterCard(monsters[idx]));
                check[idx]++;
            }
            for (int i = 0; i < 10; i++)
            {
                int idx = rd.Next(0, spellAndTraps.Count);
                while (check[idx] >= 3)
                {
                    idx = rd.Next(0, spellAndTraps.Count);
                }
                player.Deck.Insert(rd.Next(player.Deck.Count), new SpellAndTrapCard(spellAndTraps[idx]));
                check[idx]++;
            }
        }
    }

    public class PlayerCollection
    {
        private uint capacity;

        private Player[] players;

        public PlayerCollection()
        {
            capacity = 2;
            players = new Player[2];
        }

        public PlayerCollection(uint capacity)
        {
            this.capacity = capacity;
            players = new Player[capacity];
        }

        public Player this[int index]
        {
            get
            {
                if (index > capacity - 1 || index < 0)
                    return null;
                return players[index];
            }
            set
            {
                if (index > capacity - 1 || index < 0)
                    return;
                players[index] = value;
            }
        }
    }
}
