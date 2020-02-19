using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 「守备」封禁
    /// </summary>
    public class C63102017
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

            MonsterCard target = player.Enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null)
                return;
            target.Status.DefensePosition = false;
            target.Status.FaceDown = false;
        }
    }
}
