using System.Linq;
using YuGiOh2.Base;


namespace YuGiOh2.Cards
{
    /// <summary>
    /// 森
    /// </summary>
    public class C87430998
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return true;
        }

        public static void ProcessEffect(Player player)
        {
            PowerUp(player);
            PowerUp(player.Enemy);
        }

        private static void PowerUp(Player player)
        {
            player.Field.MonsterFields
                .Where(c => c != null &&
                (c.MonsterType == (int)MonsterType.Insect ||
                c.MonsterType == (int)MonsterType.Plant ||
                c.MonsterType == (int)MonsterType.Beast ||
                c.MonsterType == (int)MonsterType.BeastWarrior))
                .ToList()
                .ForEach(c => { c.ATK += (c.ATK >> 1); c.DEF += (c.DEF >> 1); });
        }

        private static void PowerDown(Player player)
        {
            player.Field.MonsterFields
                .Where(c => c != null &&
                (c.MonsterType == (int)MonsterType.Insect ||
                c.MonsterType == (int)MonsterType.Plant ||
                c.MonsterType == (int)MonsterType.Beast ||
                c.MonsterType == (int)MonsterType.BeastWarrior))
                .ToList()
                .ForEach(c => { c.ATK = (int)(c.ATK / 1.5); c.DEF = (int)(c.DEF / 1.5); });
        }

        public static void ProcessWhenSummon(string targetID, Player player)
        {
            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                player.Enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null ||
                (!(target.MonsterType == (int)MonsterType.Insect ||
                target.MonsterType == (int)MonsterType.Plant ||
                target.MonsterType == (int)MonsterType.Beast ||
                target.MonsterType == (int)MonsterType.BeastWarrior)))
                return;
            target.ATK += (target.ATK >> 1);
            target.DEF += (target.DEF >> 1);
        }

        public static void ProcessWhenSetMonster(string targetID, Player player)
        {
            ProcessWhenSummon(targetID, player);
        }

        public static void ProcessWhenLeave(string targetID, Player player)
        {
            PowerDown(player);
            PowerDown(player.Enemy);
        }
    }
}
