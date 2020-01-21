using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Data;

namespace YuGiOh2.Base
{
    public class Game
    {
        public readonly string UID;
        public readonly Player Player1;
        public readonly Player Player2;

        public Game(string id1, string id2)
        {
            UID = Guid.NewGuid().ToString();
            Player1 = new Player(null, id1);
            Player1.Lose += Player1_Lose;
            Player2 = new Player(null, id2);
            Player2.Lose += Player2_Lose;
            var c1 = DuelUtils.GetCard("89631139");
            var c2 = DuelUtils.GetCard("46986414");
            Card be = new MonsterCard(c1);
            Card dm = new MonsterCard(c2);
            Player1.Deck.Add(be);
            Player1.Deck.Add(dm);
        }

        private void Player1_Lose(object sender, EventArgs e)
        {
            
        }

        private void Player2_Lose(object sender, EventArgs e)
        {

        }
    }
}
