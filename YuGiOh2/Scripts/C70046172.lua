Type = 13;
--突进
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
        if monster ~= nil and monster.UID == targetID then
            target = monster;
        end
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            target = monster;
        end
    end
    if target == nil then
        return ;
    end
    target.ATK = target.ATK + 700;
end

function processEndPhase(targetID, player)
    if targetID == nil then
        return ;
    end
    local target = nil;
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            target = monster;
        end
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            target = monster;
        end
    end
    if target == nil then
        return ;
    end
    if target.ATK > 700 then
        target.ATK = target.ATK - 700;
    else
        target.ATK = 0;
    end
end