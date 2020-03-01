using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 地碎
    /// </summary>
    public class C97169186
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Player player)
        {
            var max = player.Enemy.Field.MonsterFields.Select(c => c == null ? 0 : c.DEF).Max();
            for (int i = 0; i < 5; i++)
            {
                if (player.Enemy.Field.MonsterFields[i] == null || 
                    player.Enemy.Field.MonsterFields[i].Status.FaceDown || 
                    player.Enemy.Field.MonsterFields[i].DEF != max)
                    continue;

                player.Enemy.AddCardToGrave(player.Enemy.Field.MonsterFields[i]);
                player.Enemy.Field.MonsterFields[i] = null;
                return;
            }
        }
    }
}
