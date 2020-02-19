using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 突进
    /// </summary>
    public class C70046172
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
            if (target == null)
                return;
            target.ATK += 700;
        }

        public static void ProcessEndPhase(string targetID, Player player)
        {
            if (targetID == null)
                return;

            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                player.Enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null)
                return;
            target.ATK = target.ATK > 700 ? target.ATK - 700 : 0;
        }
    }
}
