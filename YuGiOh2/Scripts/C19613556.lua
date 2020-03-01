Type = 0;
--大风暴
function checkIfAvailable(player)
    for index, spell in pairs(player.Field.SpellAndTrapFields) do
        if spell ~= nil and spell.UID ~= player.EffectingCard.UID then
            return true;
        end
    end
    for index, spell in pairs(player.Enemy.Field.SpellAndTrapFields) do
        if spell ~= nil then
            return true;
        end
    end
    if player.Field.FieldField ~= nil or player.Enemy.Field.FieldField ~= nil then
        return true;
    end
    return false;
end

function processEffect(player)
    destroy(player.Enemy);
    destroy(player);
end

function destroy(player)
    for i, spell in pairs(player.Field.SpellAndTrapFields) do
        if spell ~= nil then
            player:AddCardToGrave(spell);
            player:ClearSpellAndTrapField(i - 1);
        end
    end
    if player.Field.FieldField ~= nil then
        player.Enemy:ProcessEnemyField();
    end
    player:ClearTrapsWhenSummon();
    player:ClearTrapsWhenAttack();
end