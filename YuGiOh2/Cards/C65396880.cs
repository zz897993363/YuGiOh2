using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 大革命
    /// </summary>
    public class C65396880
    {
        public static int Type { get; } = (int)AffectMomentType.WhenAttacked;

        public static void ProcessEffect(string targetID, Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.MonsterFields[i] == null || 
                    player.Field.MonsterFields[i].Status.DefensePosition ||
                    player.Field.MonsterFields[i].Level > 3)
                    continue;

                Destroy(player.Enemy);
                return;
            }
            Destroy(player);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.MonsterFields[i] != null)
                {
                    player.AddCardToGrave(player.Field.MonsterFields[i]);
                    player.Field.MonsterFields[i] = null;
                }
                if (player.Field.SpellAndTrapFields[i] != null)
                {
                    player.AddCardToGrave(player.Field.SpellAndTrapFields[i]);
                    player.Field.SpellAndTrapFields[i] = null;
                }
            }
            if (player.Field.FieldField != null)
            {
                if (!player.Field.FieldField.Status.FaceDown)
                {
                    player.Enemy.ProcessEnemyField();
                }
                else
                {
                    player.Grave.Add(player.Field.FieldField);
                    player.Field.FieldField = null;
                }
            }
            player.DiscardHands(player.Hands.Count);
            player.TrapsWhenAttack.Clear();
            player.TrapsWhenSummon.Clear();
        }
    }
}
