using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 雷击
    /// </summary>
    public class C12580477
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Enemy.Field.MonsterFields.Any(c => c != null);
        }

        public static void ProcessEffect(Player player)
        {
            Destroy(player.Enemy);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.MonsterFields[i] == null)
                    continue;

                player.AddCardToGrave(ref player.Field.MonsterFields[i]);
                player.Field.MonsterFields[i] = null;
            }
        }
    }
}