using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 大风暴
    /// </summary>
    public class C19613556
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return enemy.Field.SpellAndTrapFields.Any(c => c != null) ||
                player.Field.SpellAndTrapFields.Any(c => c != null && c.UID != player.EffectingCard.UID);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            Destroy(enemy);
            Destroy(player);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.SpellAndTrapFields[i] == null)
                    continue;

                DuelUtils.ResetCard(ref player.Field.SpellAndTrapFields[i]);
                player.Grave.Add(player.Field.SpellAndTrapFields[i]);
                player.Field.SpellAndTrapFields[i] = null;
            }
            if (player.Field.FieldField != null)
            {
                if (!player.Field.FieldField.Status.FaceDown)
                {
                    player.Enemy.ProcessEnemyField();
                }
                else
                {
                    DuelUtils.ResetCard(ref player.Field.FieldField);
                    player.Grave.Add(player.Field.FieldField);
                    player.Field.FieldField = null;
                }
            }
            player.TrapsWhenAttack.Clear();
            player.TrapsWhenSummon.Clear();
        }
    }
}
