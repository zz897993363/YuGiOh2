﻿using YuGiOh2.Base;

namespace YuGiOh2.Cards
{
    /// <summary>
    /// 爱恶作剧的双子恶魔
    /// </summary>
    public class C44763025
    {
        public static int Type { get; } = (int)ChooseTargetType.None;

        public static bool CheckIfAvailable(Player player)
        {
            return player.HP > 1000;
        }

        public static void ProcessEffect(Player player)
        {
            player.DecreaseHP(1000);
            player.Enemy.DiscardHands(2);
        }
    }
}
