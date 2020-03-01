using Microsoft.EntityFrameworkCore;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Data
{
    public static class DuelUtils
    {
        public static string ScriptsPath { get; set; }

        public static DbContextOptionsBuilder<DBContext> Builder { get; set; }

        public static Models.Card GetCard(string password)
        {
            return new DBContext(Builder.Options).Card.FirstOrDefault(c => c.Password == password);
        }

        //public static Models.Card GetFusionCard(string password1, string password2)
        //{
        //    var result = new DBContext(Builder.Options).Fusion
        //        .FirstOrDefault(f => f.Password1 == password1 && f.Password2 == password2 ||
        //        f.Password1 == password2 && f.Password2 == password1);

        //    return result == null ? null : GetCard(result.PasswordResult);
        //}

        public static Card ResetCard(Card card)
        {
            string pwd = card.Password;
            string uid = card.UID;
            var card_m = new DBContext(Builder.Options).Card.FirstOrDefault(c => c.Password == pwd);
            if (card_m.Category == 0)
            {
                return new MonsterCard(card_m);
            }
            else
            {
                return new SpellAndTrapCard(card_m);
            }
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
            var deck_m = new DBContext(Builder.Options).Deck.ToList().FirstOrDefault(d => d.Id == index);
            if (deck_m == null)
                return null;

            var cards_m = new DBContext(Builder.Options).Card;
            string[] pwds = deck_m.Composition.Split(',');
            List<Card> deck = new List<Card>();
            Random rd = new Random();
            foreach (var pwd in pwds)
            {
                var card_m = cards_m.FirstOrDefault(c => c.Password == pwd);
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
            return new DBContext(Builder.Options).Card.ToList();
        }

        public static List<Models.Deck> GetAllDecks()
        {
            return new DBContext(Builder.Options).Deck.ToList();
        }

        public static void LoadScripts(Game game)
        {
            Dictionary<string, Script> scripts = new Dictionary<string, Script>();
            AddScripts(game.Player1, scripts);
            AddScripts(game.Player2, scripts);
            game.Player1.CardScripts = scripts;
            game.Player2.CardScripts = scripts;
        }

        private static void AddScripts(Player player, Dictionary<string, Script> scripts)
        {
            foreach (var card in player.Deck)
            {
                if (scripts.ContainsKey(card.Password))
                    continue;

                string path = ScriptsPath + $"C{card.Password}.lua";
                if (!System.IO.File.Exists(path))
                    continue;

                Script script = new Script();
                script.DoFile(path);
                scripts.Add(card.Password, script);
            }
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
