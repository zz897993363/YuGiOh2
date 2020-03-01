Type = 0;
--地裂
function checkIfAvailable(player)
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown then
            return true;
        end
    end
    return false;
end

function processEffect(player)
    local min = 999999;
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.ATK < min then
            min = monster.ATK;
        end
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.ATK == min then
            player.Enemy:AddCardToGrave(monster);
            player.Enemy:ClearMonsterField(i - 1);
        end
    end
end