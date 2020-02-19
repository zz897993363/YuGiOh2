using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 强欲之壶
    /// </summary>
    public class C55144522
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Deck.Count > 1;
        }

        public static void ProcessEffect(Player player)
        {
            player.DrawCard(2);
        }
    }
}
