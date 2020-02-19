using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 心变
    /// </summary>
    public class C04031928
    {
        public static int Type { get; } = (int)ChooseTargetType.FoeMonster;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Enemy.Field.MonsterFields.Any(c => c != null);
        }

        public static void ProcessEffect(string targetID, Player player)
        {
            if (targetID == null)
                return;

            for (int i = 0; i < 5; i++)
            {
                if (player.Enemy.Field.MonsterFields[i]?.UID == targetID)
                {
                    if (player.Field.MonsterFields.All(c => c != null))
                    {
                        player.Enemy.AddCardToGrave(ref player.Enemy.Field.MonsterFields[i]);
                        player.Enemy.Field.MonsterFields[i] = null;
                        return;
                    }
                    int[] sort = new int[] { 2, 1, 3, 0, 4 };
                    foreach (int num in sort)
                    {
                        if (player.Field.MonsterFields[num] == null)
                        {
                            player.Field.MonsterFields[num] = player.Enemy.Field.MonsterFields[i];
                            player.Enemy.Field.MonsterFields[i] = null;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
