Type = 9;
--心变
function checkIfAvailable(player)
    for i, v in pairs(player.Enemy.Field.MonsterFields) do
        if v ~= nil then
            return true;
        end
    end
    return false;
end

function processEffect(targetID, player)
    if targetID == nil then
        return ;
    end
    for index, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster.UID == targetID then
            if #player.Field.MonsterFields == 5 then
                player.Enemy:AddCardToGrave(monster);
                player.Enemy:ClearMonsterField(index - 1);
                return ;
            end
            local sort = { 2, 1, 3, 0, 4 };
            for i, v in pairs(sort) do
                if player.Field.MonsterFields[v] == nil then
                    player:SetMonsterField(v, monster);
                    player.Enemy:ClearMonsterField(index - 1);
                    return;
                end
            end
        end
    end
end