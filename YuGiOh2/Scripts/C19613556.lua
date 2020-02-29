Type = 0;
--大风暴
function checkIfAvailable(player)
    for index, spell in pairs(player.Field.SpellAndTrapFields) do
        if spell ~= nil and spell.UID ~= player.EffectingCard.UID then
            return true;
        end
    end
    for index, spell in pairs(player.Enemy.SpellAndTrapFields) do
        if spell ~= nil then
            return true;
        end
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
    player.TrapsWhenAttack:Clear();
    player.TrapsWhenSummon:Clear();
end