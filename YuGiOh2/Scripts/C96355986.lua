Type = 2;
--神圣标枪
function processEffect(targetID, player)
    if targetID == nil then
        return ;
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            player:IncreaseHP(monster.ATK);
        end
    end
end