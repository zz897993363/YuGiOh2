using System;
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
            Player1.Deck = DuelUtils.GetRandomDeck();
            Player2.Deck = DuelUtils.GetRandomDeck();
        }
    }

    public class PlayerCollection
    {
        private readonly uint capacity;

        private readonly Player[] players;

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
