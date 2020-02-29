Type = 1;
--落穴
function processEffect(targetID, player)
    if targetID == nil then
        return;
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID and targetID.ATK >= 1000 then
            player.Enemy:AddCardToGrave(monster);
            player.Enemy:ClearMonsterField(i - 1);
            return;
        end
    end
end