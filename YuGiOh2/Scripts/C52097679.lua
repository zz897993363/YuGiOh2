Type = 0;
--右手持盾左手持剑
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

function processEffect(player)
    change(player);
    change(player.Enemy);
end

function change(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown then
            local tmp = monster.ATK;
            monster.ATK = monster.DEF;
            monster.DEF = tmp;
        end
    end
end