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
    local target;
    local enemyGrave = {};
    for i, card in pairs(player.Enemy.Grave) do
        if card.CardCategory == 0 and card.UID == targetID then
            target = card;
        else
            table.insert(enemyGrave, card);
        end
    end
    player.Enemy.Grave = enemyGrave;
    if target == nil then
        local playerGrave = {};
        for i, card in pairs(player.Grave) do
            if card.CardCategory == 0 and card.UID == targetID then
                target = card;
            else
                table.insert(playerGrave, card);
            end
        end
        player.Grave = playerGrave;
    end
    if target == nil then
        return ;
    end
    if #player.Field.MonsterFields == 5 then
        table.insert(player.Grave, target);
        return ;
    end
    local tmp = player.CanSummon;
    player.CanSummon = true;
    local hands = {};
    for i, card in pairs(player.Hands) do
        table.insert(hands, card);
    end
    table.insert(hands, target);
    player.Hands = hands;
    player:SummonMonsterFromHands(target.UID);
    player:ProcessContinuousEffectWhenSummon(target.UID);
    player.Enemy:ProcessContinuousEffectWhenSummon(target.UID);
    target.Status.CanChangePosition = true;
    player.CanSummon = tmp;
end