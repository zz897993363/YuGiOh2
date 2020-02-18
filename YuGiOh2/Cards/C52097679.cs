using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 右手持盾左手持剑
    /// </summary>
    public class C52097679
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

                int tmp = monster.ATK;
                monster.ATK = monster.DEF;
                monster.DEF = tmp;
            }
        }
    }
}
