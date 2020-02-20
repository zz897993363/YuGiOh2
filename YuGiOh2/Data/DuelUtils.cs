using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YuGiOh2.Base;

namespace YuGiOh2.Data
{
    public class DuelUtils
    {
        public static DBContext DBContext { get; set; }

        private static List<Models.Card> _cards;

        private static List<Models.Deck> _decks;

        public static Models.Card GetCard(string password)
        {
            return DBContext.Card.FirstOrDefault(c => c.Password == password);
        }

        public static Models.Card GetFusionCard(string password1, string password2)
        {
            var result = DBContext.Fusion
                .FirstOrDefault(f => f.Password1 == password1 && f.Password2 == password2 ||
                f.Password1 == password2 && f.Password2 == password1);

            return result == null ? null : GetCard(result.PasswordResult);
        }

        public static int AddCard(Models.Card card)
        {
            DBContext.Card.Add(card);
            return DBContext.SaveChanges();
        }

        public static void ResetCard(ref Card card)
        {

            if (card is MonsterCard)
            {
                MonsterCard c = card as MonsterCard;
                ResetCard(ref c);
            }
            else if (card is SpellAndTrapCard)
            {
                SpellAndTrapCard c = card as SpellAndTrapCard;
                ResetCard(ref c);
            }
        }

        public static void ResetCard(ref MonsterCard card)
        {
            string pwd = card.Password;
            string uid = card.UID;
            var card_m = _cards.FirstOrDefault(c => c.Password == pwd);
            card = new MonsterCard(card_m);
            card.UID = uid;
        }

        public static void ResetCard(ref SpellAndTrapCard card)
        {
            string pwd = card.Password;
            string uid = card.UID;
            var card_m = _cards.FirstOrDefault(c => c.Password == pwd);
            card = new SpellAndTrapCard(card_m);
            card.UID = uid;
        }

        internal static List<Card> GetRandomDeck()
        {
            var cardBase = GetAllCards();
            var monsters = cardBase.Where(c => c.Category == (int)CardCategory.Monster).ToList();
            var spellAndTraps = cardBase.Where(c => c.Category != (int)CardCategory.Monster).ToList();
            int[] check = new int[monsters.Count + spellAndTraps.Count];
            List<Card> deck = new List<Card>();
            Random rd = new Random();
            for (int i = 0; i < 15; i++)
            {
                int idx = rd.Next(0, monsters.Count);
                while (check[idx] >= 3)
                {
                    idx = rd.Next(0, monsters.Count);
                }
                deck.Insert(rd.Next(deck.Count), new MonsterCard(monsters[idx]));
                check[idx]++;
            }
            for (int i = 0; i < 10; i++)
            {
                int idx = rd.Next(0, spellAndTraps.Count);
                while (check[idx] >= 3)
                {
                    idx = rd.Next(0, spellAndTraps.Count);
                }
                deck.Insert(rd.Next(deck.Count), new SpellAndTrapCard(spellAndTraps[idx]));
                check[idx]++;
            }
            List<Card> deck2 = new List<Card>();
            while (deck.Count > 0)
            {
                int idx = rd.Next(0, deck.Count);
                Card card = deck[idx];
                deck.Remove(card);
                deck2.Add(card);
            }
            return deck2;
        }

        internal static List<Card> GetDeck(int index)
        {
            var deck_m = _decks.FirstOrDefault(d => d.Id == index);
            if (deck_m == null)
                return null;

            string[] pwds = deck_m.Composition.Split(',');
            List<Card> deck = new List<Card>();
            Random rd = new Random();
            foreach (var pwd in pwds)
            {
                var card_m = _cards.FirstOrDefault(c => c.Password == pwd);
                if (card_m.Category == 0)
                {
                    deck.Insert(rd.Next(deck.Count), new MonsterCard(card_m));
                }
                else
                {
                    deck.Insert(rd.Next(deck.Count), new SpellAndTrapCard(card_m));
                }
            }
            List<Card> deck2 = new List<Card>();
            while (deck.Count > 0)
            {
                int idx = rd.Next(0, deck.Count);
                Card card = deck[idx];
                deck.Remove(card);
                deck2.Add(card);
            }
            return deck2;
        }

        public static List<Models.Card> GetAllCards()
        {
            if (_cards == null)
                _cards = DBContext.Card.ToList();
            return _cards;
        }

        public static List<Models.Deck> GetAllDecks()
        {
            if (_decks == null)
                _decks = DBContext.Deck.ToList();
            return _decks;
        }
    }

    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Models.Card> Card { get; set; }

        public DbSet<Models.Fusion> Fusion { get; set; }

        public DbSet<Models.Deck> Deck { get; set; }
    }
}
