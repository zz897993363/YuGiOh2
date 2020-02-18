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

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return player.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown) ||
                enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            Change(player);
            Change(enemy);
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
