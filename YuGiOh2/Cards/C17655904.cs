using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 毁灭之爆裂疾风弹
    /// </summary>
    public class C17655904
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.Enemy.Field.MonsterFields.Any(c => c != null) &&
                player.Field.MonsterFields.Any(c => c != null && c.Password == "89631139" && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Player player)
        {
            if (!player.Field.MonsterFields.Any(c => c != null && c.Password == "89631139" && !c.Status.FaceDown))
                return;

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
