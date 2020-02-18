using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 「攻击」封禁
    /// </summary>
    public class C25880422
    {
        public static int Type { get; } = (int)ChooseTargetType.FoeMonster;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            MonsterCard target = enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null)
                return;
            target.Status.DefensePosition = true;
        }
    }
}
