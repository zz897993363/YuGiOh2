using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 地裂
    /// </summary>
    public class C66788016
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            var min = enemy.Field.MonsterFields.Select(c => c == null ? int.MaxValue : c.ATK).Min();
            for (int i = 0; i < 5; i++)
            {
                if (enemy.Field.MonsterFields[i] == null || enemy.Field.MonsterFields[i].ATK != min)
                    continue;

                DuelUtils.ResetCard(ref enemy.Field.MonsterFields[i]);
                enemy.Grave.Add(enemy.Field.MonsterFields[i]);
                enemy.Field.MonsterFields[i] = null;
                return;
            }
        }
    }
}
