Type = 1;
--激流葬
function processEffect(targetID, player)
    destroy(player.Enemy);
    destroy(player);
end

function destroy(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil then
            player:AddCardToGrave(monster);
            player:ClearMonsterField(i - 1);
        end
    end
end