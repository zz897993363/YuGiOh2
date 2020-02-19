using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 心变
    /// </summary>
    public class C04031928
    {
        public static int Type { get; } = (int)ChooseTargetType.FoeMonster;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return enemy.Field.MonsterFields.Any(c => c != null);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            for (int i = 0; i < 5; i++)
            {
                if (enemy.Field.MonsterFields[i]?.UID == targetID)
                {
                    if (player.Field.MonsterFields.All(c => c != null))
                    {
                        DuelUtils.ResetCard(ref enemy.Field.MonsterFields[i]);
                        enemy.Grave.Add(enemy.Field.MonsterFields[i]);
                        enemy.Field.MonsterFields[i] = null;
                        return;
                    }
                    int[] sort = new int[] { 2, 1, 3, 0, 4 };
                    foreach (int num in sort)
                    {
                        if (player.Field.MonsterFields[num] == null)
                        {
                            player.Field.MonsterFields[num] = enemy.Field.MonsterFields[i];
                            enemy.Field.MonsterFields[i] = null;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
