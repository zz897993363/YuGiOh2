using System.Linq;
using YuGiOh2.Base;
using YuGiOh2.Data;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 死者苏生
    /// </summary>
    public class C83764718
    {
        public static int Type { get; } = (int)ChooseTargetType.AllMonsterInGrave;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return (enemy.Grave.Any(c => c.CardCategory == 0) || player.Grave.Any(c => c.CardCategory == 0)) &&
                player.Field.MonsterFields.Any(c => c == null);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            if (targetID == null)
                return;

            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (var num in sort)
            {
                if (player.Field.MonsterFields[num] != null)
                    continue;

                MonsterCard monster = (player.Grave.FirstOrDefault(c => c.UID == targetID) ?? 
                    enemy.Grave.FirstOrDefault(c => c.UID == targetID)) as MonsterCard;
                if (monster == null)
                    return;

                player.Grave.Remove(monster);
                enemy.Grave.Remove(monster);
                player.Hands.Add(monster);
                player.CanSummon = true;
                player.SummonMonsterFromHands(monster.UID);
                monster.Status.CanChangePosition = true;
            }
        }
    }
}
