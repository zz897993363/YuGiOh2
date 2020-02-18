﻿using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 巨大化
    /// </summary>
    public class C22046459
    {
        public static int Type { get; } = (int)ChooseTargetType.AllFaceUpMonster;

        public static bool CheckIfAvailable(Card card, Player player, Player enemy)
        {
            return player.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown) ||
                enemy.Field.MonsterFields.Any(c => c != null && !c.Status.FaceDown);
        }

        public static void ProcessEffect(Card card, string targetID, Player player, Player enemy)
        {
            MonsterCard target = player.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID) ??
                enemy.Field.MonsterFields.FirstOrDefault(c => c != null && c.UID == targetID);
            if (target == null || player.HP == enemy.HP)
                return;
            target.ATK = player.HP > enemy.HP ? target.ATK >> 1 : target.ATK << 1;
        }
    }
}