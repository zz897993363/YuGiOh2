using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 救世主之蚁地狱
    /// </summary>
    public class C54109233
    {
        public static int Type { get; } = (int)AffectMomentType.WhenSummoned;

        public static void ProcessEffect(string targetID, Player player)
        {
            if (targetID == null)
                return;

            for (int i = 0; i < 5; i++)
            {
                if (player.Enemy.Field.MonsterFields[i] == null ||
                    player.Enemy.Field.MonsterFields[i].UID != targetID ||
                    player.Enemy.Field.MonsterFields[i].Level > 4)
                    continue;

                player.Enemy.AddCardToGrave(player.Enemy.Field.MonsterFields[i]);
                player.Enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
