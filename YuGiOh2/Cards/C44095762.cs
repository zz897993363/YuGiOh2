using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 神圣防护罩-反射镜力-
    /// </summary>
    public class C44095762
    {
        public static int Type { get; } = (int)AffectMomentType.WhenAttacked;

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            for (int i = 0; i < 5; i++)
            {
                if (enemy.Field.MonsterFields[i] == null || enemy.Field.MonsterFields[i].Status.DefensePosition)
                    continue;

                DuelUtils.ResetCard(enemy.Field.MonsterFields[i]);
                enemy.Grave.Add(enemy.Field.MonsterFields[i]);
                enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
