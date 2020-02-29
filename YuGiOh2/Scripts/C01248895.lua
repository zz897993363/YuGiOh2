Type = 1;
--连锁破坏
function processEffect(targetID, player)
    for index, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and monster.UID == targetID and monster.ATK <= 2000 then
            local pwd = monster.Password;
            local tmpDeck = {};
            player.Enemy:AddCardToGrave(monster);
            player.Enemy:ClearMonsterField(index - 1);
            for i, v in ipairs(player.Enemy.Deck) do
                if v.Password ~= pwd then
                    table.insert(tmpDeck, v);
                else
                    player.Enemy:AddCardToGrave(v);
                end
            end
            player.Enemy.Deck = tmpDeck;
        else
            table.insert(tmpMonster, monster);
        end
    end
end