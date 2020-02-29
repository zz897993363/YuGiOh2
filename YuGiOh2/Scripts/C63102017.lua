Type = 9;
--「守备」封禁
function checkIfAvailable(player)
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil then
            return true;
        end
    end
    return false;
end

function processEffect(targetID, player)
    if targetID == nil then
        return ;
    end
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            monster.Status.DefensePosition = false;
            monster.Status.FaceDown = false;
        end
    end
end