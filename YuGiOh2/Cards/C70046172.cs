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

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return player.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown) ||
                enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            if (targetID == null)
                return;

            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null)
                return;
            target.ATK += 700;
        }

        public static void ProcessEndPhase(Card card, string targetID, Player player, Player enemy)
        {
            if (targetID == null)
                return;

            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null)
                return;
            target.ATK = target.ATK > 700 ? target.ATK - 700 : 0;
        }
    }
}
