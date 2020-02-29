Type = 2;
--神圣防护罩-反射镜力-
function processEffect(targetID, player)
    for i, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.DefensePosition  then
            player.Enemy:AddCardToGrave(monster);
            player.Enemy:ClearMonsterField(i - 1);
        end
    end
end