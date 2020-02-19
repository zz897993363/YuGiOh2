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

        public static bool CheckIfAvailable(Player player)
        {
            return (player.Enemy.Grave.Any(c => c.CardCategory == 0) || player.Grave.Any(c => c.CardCategory == 0)) &&
                player.Field.MonsterFields.Any(c => c == null);
        }

        public static void ProcessEffect(string targetID, Player player)
        {
            if (targetID == null)
                return;

            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (var num in sort)
            {
                if (player.Field.MonsterFields[num] != null)
                    continue;

                if (!((player.Grave.FirstOrDefault(c => c.UID == targetID) ??
                    player.Enemy.Grave.FirstOrDefault(c => c.UID == targetID)) is MonsterCard monster))
                    return;

                player.Grave.Remove(monster);
                player.Enemy.Grave.Remove(monster);
                player.Hands.Add(monster);
                bool tmp = player.CanSummon;
                player.CanSummon = true;
                player.SummonMonsterFromHands(monster.UID);
                player.ProcessContinuousEffectWhenSummon(monster.UID);
                player.Enemy.ProcessContinuousEffectWhenSummon(monster.UID);
                monster.Status.CanChangePosition = true;
                player.CanSummon = tmp;
            }
        }
    }
}
