using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuGiOh2.Base;

namespace YuGiOh2.Data
{
    public class DuelUtils
    {
        public static DBContext DBContext { get; set; }

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
