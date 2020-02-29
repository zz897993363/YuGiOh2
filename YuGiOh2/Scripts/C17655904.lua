Type = 0;
--毁灭之爆裂疾风弹
function checkIfAvailable(player)
    for index, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and monster.Password == '89631139' and not monster.Status.FaceDown then
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
        if monster ~= nil and monster.Password == '89631139' and not monster.Status.FaceDown then
            destroy(player.Enemy);
        end
    end
end

function destroy(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil then
            player:AddCardToGrave(monster);
            player:ClearMonsterField(i - 1);
        end
    end
end