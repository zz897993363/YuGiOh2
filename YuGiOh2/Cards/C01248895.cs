using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 连锁破坏
    /// </summary>
    public class C01248895
    {
        public static int Type { get; set; } = (int)AffectMomentType.WhenSummoned;

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            for (int i = 0; i < 5; i++)
            {
                if (enemy.Field.MonsterFields[i] == null ||
                    enemy.Field.MonsterFields[i].UID != targetID ||
                    enemy.Field.MonsterFields[i].ATK > 2000)
                    continue;

                DuelUtils.ResetCard(enemy.Field.MonsterFields[i]);
                var result = enemy.Deck.Where(c => c.Password == enemy.Field.MonsterFields[i].Password);
                foreach (var item in result)
                {
                    enemy.Deck.Remove(item);
                }
                enemy.Grave.AddRange(result);
                enemy.Grave.Add(enemy.Field.MonsterFields[i]);
                enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
