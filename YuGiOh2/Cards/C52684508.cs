using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 黑炎弹
    /// </summary>
    public class C52684508
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return player.Field.MonsterFields.Any(c => c != null && c.Password == "74677422" && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            var monster = player.Field.MonsterFields
                .FirstOrDefault(c => c != null && c.Password == "74677422" && !c.Status.FaceDown);
            if (monster == null)
                return;
            enemy.DecreaseHP(monster.ATK);
        }
    }
}
