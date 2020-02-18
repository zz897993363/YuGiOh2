using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 神圣标枪
    /// </summary>
    public class C96355986
    {
        public static int Type { get; } = (int)AffectMomentType.WhenAttacked;
        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            var monster = enemy.Field.MonsterFields.FirstOrDefault(c => c.UID == targetID);
            if (monster == null)
                return;

            player.IncreaseHP(monster.ATK);
        }
    }
}
