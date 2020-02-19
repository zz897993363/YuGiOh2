using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 巨大化
    /// </summary>
    public class C22046459
    {
        public static int Type { get; } = (int)ChooseTargetType.AllFaceUpMonster;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown) ||
                player.Enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(string targetID, Player player)
        {
            if (targetID == null)
                return;

            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                player.Enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null || player.HP == player.Enemy.HP)
                return;
            target.ATK = player.HP > player.Enemy.HP ? target.ATK >> 1 : target.ATK << 1;
        }
    }
}
