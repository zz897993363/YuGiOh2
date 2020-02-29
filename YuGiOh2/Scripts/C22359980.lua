Type = 2;
--银幕之镜壁
function processEffect(targetID, player)
    for index, monster in pairs(player.Enemy.Field.MonsterFields) do
        if monster ~= nil and not monster.Status.FaceDown then
            monster.ATK = monster.ATK / 2;
        end
    end
end