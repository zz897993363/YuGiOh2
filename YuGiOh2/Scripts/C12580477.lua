Type = 0;
--雷击
function checkIfAvailable(player)
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil then
            return true;
        end
    end
    return false;
end

function processEffect(player)
    destroy(player.Enemy);
end

function destroy(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil then
            player:AddCardToGrave(monster);
            player:ClearMonsterField(i - 1);
        end
    end
end