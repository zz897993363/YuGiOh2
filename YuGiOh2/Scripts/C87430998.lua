Type = 0;
--森
function checkIfAvailable(player)
    return true;
end

function processEffect(player)
    powerUp(player);
    powerUp(player.Enemy);
end

function powerUp(player)
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and (monster.MonsterType == 4 or monster.MonsterType == 5 or
                monster.MonsterType == 9 or monster.MonsterType == 18) then
            monster.ATK = monster.ATK + monster.ATK / 2;
            monster.DEF = monster.DEF + monster.DEF / 2;
        end
    end
end

function powerDown(player)
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and (monster.MonsterType == 4 or monster.MonsterType == 5 or
                monster.MonsterType == 9 or monster.MonsterType == 18) then
            monster.ATK = monster.ATK / 1.5;
            monster.DEF = monster.DEF / 1.5;
        end
    end
end

function processWhenSummon(targetID, player)
    local target = nil;
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            target = monster;
        end
    end
    for index, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            target = monster;
        end
    end
    if target ~= nil and (target.MonsterType == 4 or target.MonsterType == 5 or
            target.MonsterType == 9 or target.MonsterType == 18) then
        target.ATK = target.ATK + target.ATK / 2;
        target.DEF = target.DEF + target.DEF / 2;
    end
end

function processWhenSetMonster(targetID, player)
    processWhenSummon(targetID, player);
end

function processWhenSelfLeave(targetID, player)
    powerDown(player);
    powerDown(player.Enemy);
end
