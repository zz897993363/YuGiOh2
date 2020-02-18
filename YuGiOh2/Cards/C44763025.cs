using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 爱恶作剧的双子恶魔
    /// </summary>
    public class C44763025
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return true;
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            enemy.DiscardHands(2);
        }
    }
}
