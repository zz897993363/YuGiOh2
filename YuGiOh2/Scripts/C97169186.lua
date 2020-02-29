Type = 0;
--地碎
function checkIfAvailable(player)
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown then
            return true;
        end
    end
    return false;
end

function processEffect(player)
    if targetID == nil then
        return ;
    end
    local max = 0;
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.ATK > max then
            max = monster.ATK;
        end
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.ATK == max then
            player.Enemy:AddCardToGrave(monster);
            player.Enemy:ClearMonsterField(i - 1);
        end
    end
end