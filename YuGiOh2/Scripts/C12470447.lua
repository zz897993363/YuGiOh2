Type = 0;
--邪恶的仪式
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
            monster.Status.DefensePosition = not monster.Status.DefensePosition;
            monster.Status.CanChangePosition = false;
        end
    end
end