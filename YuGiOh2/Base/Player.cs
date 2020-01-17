using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Data;

namespace YuGiOh2.Base
{
    public class Player
    {
        public string ID { get; private set; }
        public int HP { get; set; }
        public Card[] Fields { get; set; }
        public List<Card> Grave { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> Hands { get; set; }

        public Player(Card[] cards, int hp = 8000)
        {
            ID = new Guid().ToString();
            HP = hp;
            Fields = new Card[10];
            Grave = new List<Card>();
            Deck = new List<Card>(cards);
        }

        public void DrawPhase()
        {
            while (Hands.Count < 5)
            {
                DrawCard();
            }
        }

        public void EndPhase()
        {
            while (Hands.Count > 5)
            {
                DiscardHands();
            }
        }

        public void DrawCard(int count = 1)
        {
            while (count-- > 0)
            {
                if (Deck.Count == 0)
                {
                    Lose(this, null);
                    return;
                }
                Card card = Deck[Deck.Count - 1];
                Deck.Remove(card);
                Hands.Add(card);
            }
        }

        public void DiscardHands(int count = 1)
        {
            while (Hands.Count > 0 && count-- > 0)
            {
                Card card = Hands[Deck.Count - 1];
                Hands.Remove(card);
                Grave.Add(card);
            }
        }

        public void IncreaseHP(int point)
        {
            HP += point;
            if (HP <= 0)
                Lose(this, null);
        }

        public void DecreaseHP(int point)
        {
            IncreaseHP(-point);
        }

        public void SummonMonsterFromHands(string UID, int fieldIndex)
        {
            Card card = Hands.FirstOrDefault(c => c.UID == UID);
            Hands.Remove(card);
            if (Fields[fieldIndex] != null)
                Fusion(card, fieldIndex);
            else
                Fields[fieldIndex] = card;
        }

        public void Fusion(Card card1, int fieldIndex)
        {
            var card_m = DuelUtils.GetFusionCard(card1.Password, Fields[fieldIndex].Password);
            Fields[fieldIndex] = card_m == null ? card1 : new MonsterCard(card_m);
        }

        public event EventHandler Lose;
    }
}
