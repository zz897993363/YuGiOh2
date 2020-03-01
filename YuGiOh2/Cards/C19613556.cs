using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 大风暴
    /// </summary>
    public class C19613556
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Enemy.Field.SpellAndTrapFields.Any(c => c != null) ||
                player.Field.SpellAndTrapFields.Any(c => c != null && c.UID != player.EffectingCard.UID);
        }

        public static void ProcessEffect(Player player)
        {
            Destroy(player.Enemy);
            Destroy(player);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.SpellAndTrapFields[i] == null)
                    continue;

                player.AddCardToGrave(player.Field.SpellAndTrapFields[i]);
                player.Field.SpellAndTrapFields[i] = null;
            }
            if (player.Field.FieldField != null)
            {
                player.Enemy.ProcessEnemyField();
            }
            player.TrapsWhenAttack.Clear();
            player.TrapsWhenSummon.Clear();
        }
    }
}
