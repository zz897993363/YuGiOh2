Type = 1;
--救世主之蚁地狱
function processEffect(targetID, player)
    if targetID == nil then
        return ;
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID and monster.Level <= 4 then
            player.Enemy:AddCardToGrave(monster);
            player.Enemy:ClearMonsterField(i - 1);
        end
    end
end