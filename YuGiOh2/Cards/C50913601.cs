﻿using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 山
    /// </summary>
    public class C50913601
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return true;
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            PowerUp(player);
            PowerUp(enemy);
        }

        private static void PowerUp(Player player)
        {
            player.Field.MonsterFields
                .Where(c => c != null &&
                (c.MonsterType == (int)MonsterType.Dragon ||
                c.MonsterType == (int)MonsterType.WingedBeast ||
                c.MonsterType == (int)MonsterType.Thunder))
                .ToList()
                .ForEach(c => { c.ATK += (c.ATK >> 1); c.DEF += (c.DEF >> 1); });
        }

        private static void PowerDown(Player player)
        {
            player.Field.MonsterFields
                .Where(c => c != null &&
                (c.MonsterType == (int)MonsterType.Dragon ||
                c.MonsterType == (int)MonsterType.WingedBeast ||
                c.MonsterType == (int)MonsterType.Thunder))
                .ToList()
                .ForEach(c => { c.ATK = (int)(c.ATK / 1.5); c.DEF = (int)(c.DEF / 1.5); });
        }

        public static void ProcessWhenSummon(Card card, string targetID, Player player, Player enemy)
        {
            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null ||
                (!(target.MonsterType == (int)MonsterType.Dragon ||
                target.MonsterType == (int)MonsterType.WingedBeast ||
                target.MonsterType == (int)MonsterType.Thunder)))
                return;
            target.ATK += (target.ATK >> 1);
            target.DEF += (target.DEF >> 1);
        }

        public static void ProcessWhenSetMonster(Card card, string targetID, Player player, Player enemy)
        {
            ProcessWhenSummon(card, targetID, player, enemy);
        }

        public static void ProcessWhenLeave(Card card, string targetID, Player player, Player enemy)
        {
            PowerDown(player);
            PowerDown(enemy);
        }
    }
}
