using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 毁灭之爆裂疾风弹
    /// </summary>
    public class C17655904
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return enemy.Field.MonsterFields.Any(c => c != null) &&
                player.Field.MonsterFields.Any(c => c != null && c.Password == "89631139" && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            Destroy(enemy);
        }

        private static void Destroy(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.Field.MonsterFields[i] == null)
                    continue;

                DuelUtils.ResetCard(player.Field.MonsterFields[i]);
                player.Grave.Add(player.Field.MonsterFields[i]);
                player.Field.MonsterFields[i] = null;
            }
        }
    }
}
