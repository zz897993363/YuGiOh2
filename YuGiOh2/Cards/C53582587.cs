using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 激流葬
    /// </summary>
    public class C53582587
    {
        public static int Type { get; } = (int)AffectMomentType.WhenSummoned;

        public static void ProcessEffect(string targetID, Player player)
        {
            Destroy(player);
            Destroy(player.Enemy);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.MonsterFields[i] == null)
                    continue;

                player.AddCardToGrave(player.Field.MonsterFields[i]);
                player.Field.MonsterFields[i] = null;
            }
        }
    }
}
