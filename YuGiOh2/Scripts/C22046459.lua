Type = 13;
--巨大化
function checkIfAvailable(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown then
            return true;
        end
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown then
            return true;
        end
    end
    return false;
end

function processEffect(targetID, player)
    if targetID == nil then
        return ;
    end
    local target = nil;
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown and monster.UID == targetID then
            target = monster;
        end
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown and monster.UID == targetID then
            target = monster;
        end
    end
    if target == nil or player.HP == player.Enemy.HP then
        return ;
    end
    if player.HP > player.Enemy.HP then
        target.ATK = target.ATK / 2;
    else
        target.ATK = target.ATK * 2;
    end
end