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

        public static void ProcessEffect(string targetID, Player player)
        {
            if (targetID == null)
                return;

            var monster = player.Enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (monster == null)
                return;

            player.IncreaseHP(monster.ATK);
        }
    }
}
