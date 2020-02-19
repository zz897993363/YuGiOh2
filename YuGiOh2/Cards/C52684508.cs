using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 黑炎弹
    /// </summary>
    public class C52684508
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Field.MonsterFields.Any(c => c != null && c.Password == "74677422" && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Player player)
        {
            var monster = player.Field.MonsterFields
                .FirstOrDefault(c => c != null && c.Password == "74677422" && !c.Status.FaceDown);
            if (monster == null)
                return;
            player.Enemy.DecreaseHP(monster.ATK);
        }
    }
}
