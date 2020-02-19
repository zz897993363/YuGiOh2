using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 激流葬
    /// </summary>
    public class C53582587
    {
        public static int Type { get; } = (int)AffectMomentType.WhenSummoned;

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            Destroy(player);
            Destroy(enemy);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.MonsterFields[i] == null)
                    continue;

                DuelUtils.ResetCard(ref player.Field.MonsterFields[i]);
                player.Grave.Add(player.Field.MonsterFields[i]);
                player.Field.MonsterFields[i] = null;
            }
        }
    }
}
