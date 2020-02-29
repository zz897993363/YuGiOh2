Type = 2;
--大革命
function ProcessEffect(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.DefensePosition and monster.Level < 4 then
            destroy(player.Enemy);
            return ;
        end
    end
    destroy(player);
end

function destroy(player)
    for i, monster in pairs(player.Field.MonsterFields) do
        if monster ~= nil then
            player:AddCardToGrave(monster);
            player:ClearMonsterField(i - 1);
        end
    end
    for i, spell in pairs(player.Field.SpellAndTrapFields) do
        if spell ~= nil then
            player:AddCardToGrave(spell);
            player:ClearSpellAndTrapField(i - 1);
        end
    end
    player:DiscardHands(player.Hands.Count);
    player.TrapsWhenAttack:Clear();
    player.TrapsWhenSummon:Clear();
end