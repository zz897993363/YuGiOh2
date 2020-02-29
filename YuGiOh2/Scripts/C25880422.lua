Type = 7;
--「攻击」封禁
function checkIfAvailable(player)
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
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID then
            monster.Status.DefensePosition = true;
        end
    end
end