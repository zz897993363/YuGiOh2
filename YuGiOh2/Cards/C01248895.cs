using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 连锁破坏
    /// </summary>
    public class C01248895
    {
        public static int Type { get; set; } = (int)AffectMomentType.WhenSummoned;

        public static void ProcessEffect(string targetID, Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Enemy.Field.MonsterFields[i] == null ||
                    player.Enemy.Field.MonsterFields[i].UID != targetID ||
                    player.Enemy.Field.MonsterFields[i].ATK > 2000)
                    continue;

                var result = player.Enemy.Deck.Where(c => c.Password == player.Enemy.Field.MonsterFields[i].Password).ToList();
                for (int j = result.Count - 1; j >= 0 ; j--)
                {
                    var card = result[j];
                    player.Enemy.AddCardToGrave(ref card);
                    player.Enemy.Deck.RemoveAt(j);
                }
                player.Enemy.AddCardToGrave(ref player.Enemy.Field.MonsterFields[i]);
                player.Enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
