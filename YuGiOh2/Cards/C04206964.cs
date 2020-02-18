using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 落穴
    /// </summary>
    public class C04206964
    {
        public static int Type { get; } = (int)AffectMomentType.WhenSummoned;
        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            for (int i = 0; i < 5; i++)
            {
                if (enemy.Field.MonsterFields[i] == null ||
                    enemy.Field.MonsterFields[i].UID != targetID ||
                    enemy.Field.MonsterFields[i].ATK < 1000)
                    continue;

                DuelUtils.ResetCard(enemy.Field.MonsterFields[i]);
                enemy.Grave.Add(enemy.Field.MonsterFields[i]);
                enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
