﻿using System.Linq;
using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 银幕之镜壁
    /// </summary>
    public class C22359980
    {
        public static int Type { get; } = (int)AffectMomentType.WhenAttacked;

        public static void ProcessEffect(string targetID, Player player)
        {
            player.Enemy.Field.MonsterFields
                .Where(c => c != null && !c.Status.FaceDown)
                .ToList()
                .ForEach(c => c.ATK >>= 1);
        }
    }
}
