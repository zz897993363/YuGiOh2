Type = 0;
--ǿ��֮��
function checkIfAvailable(player)
	return #player.Deck > 0;
end

function processEffect(player)
	player:DrawCard(2);
end
