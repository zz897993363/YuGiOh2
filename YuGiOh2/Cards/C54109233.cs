using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 救世主之蚁地狱
    /// </summary>
    public class C54109233
    {
        public static int Type { get; } = (int)AffectMomentType.WhenSummoned;

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            if (targetID == null)
                return;

            for (int i = 0; i < 5; i++)
            {
                if (enemy.Field.MonsterFields[i] == null ||
                    enemy.Field.MonsterFields[i].UID != targetID ||
                    enemy.Field.MonsterFields[i].Level > 4)
                    continue;

                DuelUtils.ResetCard(ref enemy.Field.MonsterFields[i]);
                enemy.Grave.Add(enemy.Field.MonsterFields[i]);
                enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
