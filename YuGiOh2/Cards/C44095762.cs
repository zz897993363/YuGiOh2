using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 神圣防护罩-反射镜力-
    /// </summary>
    public class C44095762
    {
        public static int Type { get; } = (int)AffectMomentType.WhenAttacked;

        public static void ProcessEffect(string targetID, Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Enemy.Field.MonsterFields[i] == null || player.Enemy.Field.MonsterFields[i].Status.DefensePosition)
                    continue;

                player.Enemy.AddCardToGrave(player.Enemy.Field.MonsterFields[i]);
                player.Enemy.Field.MonsterFields[i] = null;
            }
        }
    }
}
