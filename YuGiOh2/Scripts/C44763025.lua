Type = 0;
--爱恶作剧的双子恶魔
function checkIfAvailable(player)
    return player.HP > 1000;
end

function processEffect(player)
    player:DecreaseHP(1000);
    player.Enemy:DiscardHands(2);
end