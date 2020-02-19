using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 邪恶的仪式
    /// </summary>
    public class C12470447
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown) ||
                player.Enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Player player)
        {
            Change(player);
            Change(player.Enemy);
        }

        private static void Change(Player player)
        {
            foreach (var monster in player.Field.MonsterFields)
            {
                if (monster == null || monster.Status.FaceDown)
                    continue;
                monster.Status.DefensePosition = !monster.Status.DefensePosition;
            }
        }
    }
}
