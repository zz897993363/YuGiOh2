Type = 0;
--黑炎弹
function checkIfAvailable(player)
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and monster.Password == '74677422' and not monster.Status.FaceDown then
            return true;
        end
    end
    return false;
end

function processEffect(player)
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and monster.Password == '74677422' and not monster.Status.FaceDown then
            player.Enemy:DecreaseHP(monster.ATK);
        end
    end
end