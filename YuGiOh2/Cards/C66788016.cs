using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 地裂
    /// </summary>
    public class C66788016
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Player player)
        {
            var min = player.Enemy.Field.MonsterFields.Select(c => c == null ? int.MaxValue : c.ATK).Min();
            for (int i = 0; i < 5; i++)
            {
                if (player.Enemy.Field.MonsterFields[i] == null || 
                    player.Enemy.Field.MonsterFields[i].Status.FaceDown ||
                    player.Enemy.Field.MonsterFields[i].ATK != min)
                    continue;

                player.Enemy.AddCardToGrave(ref player.Enemy.Field.MonsterFields[i]);
                player.Enemy.Field.MonsterFields[i] = null;
                return;
            }
        }
    }
}
