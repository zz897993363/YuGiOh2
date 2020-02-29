Type = 25;
--死者苏生
function checkIfAvailable(player)
    if #player.Field.MonsterFields == 5 then
        return false;
    end
    for i, card in pairs(player.Enemy.Grave) do
        if card.CardCategory == 0 then
            return true;
        end
    end
    for i, card in pairs(player.Grave) do
        if card.CardCategory == 0 then
            return true;
        end
    end
    return false;
end

function processEffect(targetID, player)
    if targetID == nil then
        return ;
    end
    local target = nil;
    local tmpEnemy = {};
    for i, card in pairs(player.Enemy.Grave) do
        if card.CardCategory == 0 and card.UID == targetID then
            target = card;
        else
            table.insert(tmpEnemy, card);
        end
    end
    player.Enemy.Grave = tmpEnemy;
    local tmpPlayer = {};
    for i, card in pairs(player.Grave) do
        if card.CardCategory == 0 and card.UID == targetID then
            target = card;
        else
            table.insert(tmpPlayer, card);
        end
    end
    player.Grave = tmpPlayer;
    if target == nil then
        return ;
    end
    if #player.Field.MonsterFields == 5 then
        player.Grave:Add(target);
    end
    local sort = { 2, 1, 3, 0, 4 };
    for i, v in pairs(sort) do
        if player.MonsterFields[v] == nil then
            player:SetMonsterField(v, target);
            return ;
        end
    end
end