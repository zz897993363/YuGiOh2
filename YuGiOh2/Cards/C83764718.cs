using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 死者苏生
    /// </summary>
    public class C83764718
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return enemy.Grave.Count > 0 || player.Grave.Count > 0;
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {

        }
    }
}
