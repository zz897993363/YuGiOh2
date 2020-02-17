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

        public static void ResetCard(Card card)
        {
            var card_m = _cards.FirstOrDefault(c => c.Password == card.Password);
            if (card.CardCategory == 0)
            {
                card = new MonsterCard(card_m);
            }
            else
            {
                card = new SpellAndTrapCard(card_m);
            }
        }

        public static List<Models.Card> GetAllCards()
        {
            if (_cards == null)
                _cards = DBContext.Card.ToList();
            return _cards;
        }

        internal static void ProcessEffect(string cardID, Player player1, Player player2)
        {
            Card card = player1.Field.SpellAndTrapFields.FirstOrDefault(c => c.UID == cardID);
            string className = "C" + card.Password;
            Type type = Type.GetType(className);
            MethodInfo methodInfo = type.GetMethod("ProcessEffect");
            methodInfo.Invoke(null, new object[] { card, player1, player2 });
        }
    }

    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Models.Card> Card { get; set; }

        public DbSet<Models.Fusion> Fusion { get; set; }
    }
}
