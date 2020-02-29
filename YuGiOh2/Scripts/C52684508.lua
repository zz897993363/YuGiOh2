Type = 0;
--黑炎弹
function checkIfAvailable(player)
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and monster.Password == '74677422' and not monster.Status.FaceDown then
            for i, m in pairs(player.Enemy.MonsterFields) do
                if monster ~= nil then
                    return true;
                end
            end
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