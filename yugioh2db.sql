/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 5.7.24-log : Database - yugioh2
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`yugioh2` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;

USE `yugioh2`;

/*Table structure for table `Card` */

DROP TABLE IF EXISTS `Card`;

CREATE TABLE `Card` (
  `password` varchar(10) NOT NULL,
  `category` tinyint(4) NOT NULL,
  `text` varchar(500) DEFAULT NULL,
  `name` varchar(30) NOT NULL,
  `attribute` int(11) NOT NULL,
  `level` int(10) unsigned NOT NULL,
  `monstertype` int(10) unsigned NOT NULL,
  `cardtype` int(10) unsigned NOT NULL,
  `atk` int(10) unsigned NOT NULL,
  `def` int(10) unsigned NOT NULL,
  `summonedattribute` int(10) unsigned NOT NULL,
  `icon` int(11) NOT NULL,
  `cname` varchar(30) DEFAULT NULL,
  `ctext` varchar(500) DEFAULT NULL,
  `choosetargettype` int(10) unsigned zerofill NOT NULL,
  PRIMARY KEY (`password`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4;

/*Data for the table `Card` */

insert  into `Card`(`password`,`category`,`text`,`name`,`attribute`,`level`,`monstertype`,`cardtype`,`atk`,`def`,`summonedattribute`,`icon`,`cname`,`ctext`,`choosetargettype`) values 
('89631139',0,'This legendary dragon is a powerful engine of destruction. Virtually invincible, very few have faced this awesome creature and lived to tell the tale.','Blue-Eyes White Dragon',1,8,1,1,3000,2500,2,0,'青眼白龙','以高攻击力著称的传说之龙。任何对手都能粉碎，其破坏力不可估量。',0000000000),
('46986414',0,'The ultimate wizard in terms of attack and defense.','Dark Magician',0,7,0,1,2500,2100,1,0,'黑魔术师','作为魔法师，攻击力·守备力都是最高级别。',0000000000),
('14015067',0,'This creature adopts the form of a white goat living in the forest, but is actually a Forest Elder.','Ancient One of the Deep Forest',2,6,5,1,1800,1900,6,0,'大森林的长老','很久以前住在森林里的白羊，真身是森林长老。',0000000000),
('17655904',1,'','Burst Stream of Destruction',7,0,24,1,0,0,0,0,'毁灭之爆裂疾风弹','我方场上时有青眼白龙时，破坏对手场上所有怪兽。发动此卡后本回合青眼白龙不可攻击。',0000000000),
('76232340',0,'An unstoppable savage that carries Millennium Items.','Sengenjin',2,8,4,1,2750,2500,11,0,'千年原人','任何时候都以力量坚持到底，拥有千年宝物的原始人。',0000000000),
('28913279',0,'A reptilian creature that is said to be the ruler of all fairies.It is a rare being that is not commonly seen.','Ruklamba the Spirit King',0,8,11,1,1000,2000,4,0,'精灵王 路克兰巴','统领一切的精灵，相当罕见。',0000000000),
('91998119',0,'A machine that can destroy a monster on the opponent\'s field by discarding the far left card in the own hand.','XYZ-Dragon Cannon',1,8,19,2,2800,2600,2,0,'XYZ-神龙炮','拥有丢弃自己最左边的手牌破坏对手战场一张怪兽卡的效果。',0000000000),
('98502113',0,'A Warrior that can destroy a spell on the opponent\'s field by discarding the far left card in the own hand.','Dark Paladin',0,8,3,2,2900,2400,1,0,'超魔导剑士-黑暗帕拉丁','拥有丢弃自己最左边的手牌破坏对手战场一张怪兽卡的效果。',0000000000),
('39111158',0,'An unworthy dragon with three sharp horns sprouting from its head.','Tri-Horned Dragon',0,8,1,1,2850,2350,11,0,'三角魔龙','以头上生长的3只角为特征的恶魔龙。',0000000000),
('66889139',0,'A powerful knight that sits astride its dragon steer.The knight\'s power and the dragon\'s speed combine for best effect.','Gaia the Dragon Champion',5,8,1,1,2600,2100,7,0,'龙骑士盖亚','骑着龙的强力骑士。龙的速度加上骑士的力量，给与对手猛烈一击。',0000000000),
('74677422',0,'A ferocious dragon with a deadly attack.','Red-Eyes B. Dragon',0,7,1,1,2400,2000,3,0,'真红眼黑龙','拥有真红之眼的黑龙。愤怒的黑炎会把映入其眼者全部烧成灰烬。',0000000000),
('52684508',1,'','Inferno Fire Blast',7,0,24,1,0,0,0,0,'黑炎弹','我方场上有真红眼黑龙的场合，给与对手其攻击力数值的伤害。',0000000000),
('55908887',0,'An earth warrior clad in armor and bearing a huge sword.\nIt takes great pride in its strength.','Orgoth the Relentless',2,7,3,1,2500,2450,8,0,'神·奥伽斯','操纵巨大的剑的战士。对自己的力量很得意。',0000000000),
('68171737',0,'A dragon formed by huge boulders that are also used to attack the enemy.','Stone Dragon',2,7,17,1,2000,2300,8,0,'岩石龙','完全由岩石组成的龙。岩石的攻击很强。',0000000000),
('54752875',0,'A Thunder dragon that has grown an additional head.lt looses devastating thunderbolts to attack.',' Twin-Headed Thunder Dragon',1,7,16,1,2800,2100,9,0,'双头雷龙','有两个头的闪电龙。用强力的电击攻击。',0000000000),
('70781052',0,'A fiend with dark powers for confusing the enemy. Among the Fiend-Type monsters, this monster boasts considerable force.',' Twin-Headed Thunder Dragon',0,6,7,1,2500,1200,3,0,'恶魔召唤','使用黑暗力量，迷惑人心的恶魔。在恶魔族中以相当强大的力量著称。',0000000000),
('75390004',0,'A dinosaur with horns protruding from all over its body.As expected,its ramming attacks are very powerful.','Megazowler',2,6,10,1,1800,2000,8,0,'角龙','全身长满角的恐龙。突击攻击很强烈！',0000000000),
('94905343',0,'A fearsome monster that is made of a centaur and an ox.It savagely doles out very punishing attacks.','Rabid Horseman',2,6,4,1,2000,1700,6,0,'牛头人马兽','牛与人马合一的怪物。攻击力非常强劲。',0000000000),
('67371383',0,'On land or in the sea, the speed of this monster is unmatchable.','Amphibian Beast',3,6,12,1,2400,2000,10,0,'半鱼兽','在陆地像兽一样，在海里像鱼一样都能快速攻击。',0000000000),
('78984772',0,'Two dragons fused as one from the effects of the Big Bang.','Twin-Headed Fire Dragon',4,6,15,1,2200,1700,5,0,'爆炎龙','宇宙形成时所诞生的龙，那股冲击力使双胞胎龙合体。',0000000000),
('48649353',0,'A bull fiend restored by the dark arts, this monster appears out of a jar.','Ushi Oni',0,6,7,1,2150,1950,6,0,'牛鬼','通过黑魔术苏醒的牛之恶魔。从壶中现身。',0000000000),
('86164529',0,'A furtive dragon that lurks quietly out of sight underwater.It attacks by shooting blocks of water from its mouth.','Aqua Dragon',3,6,13,1,2250,1900,10,0,'水精龙','潜伏在水中的龙，从口中吐出块状的水攻击',0000000000),
('74703140',0,'A eagle that renders judgments from a lofty position.It punishes those it finds guility with silver talons.','Punished Eagle',5,6,6,1,2100,1800,7,0,'裁决之鹰','用白银爪审判恶人，飞舞的裁判官。',0000000000),
('46696593',0,'An inferno of a bird that blazes wildly in crimson all over.It looses a shower of embers with every flap of its wings.','Crimson Sunbird',4,6,6,1,2300,1800,5,0,'红阳鸟','全身燃烧着通红的火焰，一拍打翅膀就散发火之粉',0000000000),
('69780745',0,'A wicked beast that resembles a winged lion.','Garvas',2,6,5,1,2000,1700,6,0,'加尔瓦斯','恶之化身。样子如同长着羽毛的狮子。',0000000000),
('28563545',0,'An enormous fiend that is a scourge to dragons of all kinds.It can destroy every dragon on the opponent\'s field.','Dragon Seeker',0,6,7,1,2000,2100,3,0,'杀龙者','拥有破坏对手战场全部龙族怪兽的效果。',0000000000),
('09540040',0,'A stone turtle that is nearly indestructible.','Boulder Tortoise',3,6,14,1,1450,2200,8,0,'岩石龟','全身由岩石做成的龟。特点是守备力非常高。',0000000000),
('14575467',0,'The two are so close\nThey die and return to life\nInseparable','Zombino',2,4,2,1,2000,0,4,0,'僵尸男孩','两个人\n特别要好\n死了也相随\n活了也相伴\n不离不弃\n所以 两个人永远 \n都不会再次相见',0000000000),
('32485271',0,'A dark being that makes its home among beautiful rose flowers.It drains the souls of its victims to feed the plant.','Rose Spectre of Dunn',0,6,18,1,2000,1800,3,0,'栖身蔷薇的恶灵','栖息在蔷薇上的恶灵。吸收灵魂变成花的养分。',0000000000),
('45231177',0,'A swordsman that bears a fiery sword deadly to all dinosaurs.It has the power to wipe out all dinosaurs on the foe\'s field.','Flame Swordsman',4,5,3,1,1800,1600,5,0,'炎之剑士','拥有破坏对手场上所有恐龙族怪兽的效果。',0000000000),
('28546905',0,'Manipulates enemy attacks with the power of illusion.','Illusionist Faceless Mage',0,5,0,1,1200,2200,4,0,'无脸幻想师','拥有使对手战场的全部怪兽变更表示的效果。',0000000000),
('35565537',0,'A popular creature in mythology that delivers fatal attacks with a sharp spear.','Dark Witch',1,5,8,1,1800,1700,2,0,'女武神','在神话里出现的战斗天使，用手中的枪处以天罚。',0000000000),
('75850803',0,'A sorcerer that draws its power from the lunar landscape.','La Moon',1,5,0,1,1200,1700,2,0,'月之魔法师','住在月亮上的魔法师。用月之魔力迷惑对方。',0000000000),
('70345785',0,'This monster has three fire-breathing heads and can form a sea of blazing flames.','Yamadoran',4,5,1,1,1600,1800,11,0,'三头巨龙','用三个头不断地喷火，使周围化为一片火海以攻击敌人。',0000000000),
('32012841',0,'A famous shield said to belong to an ancient Egyptian Pharaoh. Legends tell of its power to block any strong attack.','Millennium Shield',2,5,3,1,0,3000,11,0,'千年盾','从古代埃及王族传下的传说之盾，据说无论什么样的强力攻击都能够抵御。',0000000000),
('11250655',0,'A reptilian monster that sprays fire in every direction.','Emperor of the Land and Sea',3,5,11,1,1800,1500,10,0,'水陆的帝王','可以用大嘴向四方喷火的爬虫怪。',0000000000),
('12146024',0,'After rendering an opponent immobile by spitting a sticky goo, this monster closes in for the attack.','Bolt Escargot',3,5,16,1,1400,1500,9,0,'电击蜗牛','吐出粘液然后用电击对方。',0000000000),
('80141480',0,'This monster feeds on whatever it catches in its web.','Hunter Spider',2,5,9,1,1600,1400,6,0,'猎手蜘蛛','布置蜘蛛网狩猎，落入网中的东西将被吃掉。',0000000000),
('44865098',0,'Guardian of the Machine Master, it crushes opposition by rolling over them.','Cyber Soldier',0,5,19,1,1500,1700,3,0,'机械士兵','护卫机械王的兵队。圆圆的身体到处转动着进行巡逻。',0000000000),
('76184692',0,'A one-eyed behemoth with thick, powerful arms made for delivering punishing blows.','Hitotsu-Me Giant',2,4,4,1,1200,1000,3,0,'独眼巨人','只长有一只眼睛的巨人。利用巨腕殴打敌人，值得小心。',0000000000),
('87564352',0,'A dragon that dwells in the depths of darkness, its vulnerability lies in its poor eyesight.','Blackland Fire Dragon',0,4,1,1,1500,800,1,0,'暗黑之龙王','生活在黑暗深处的龙，由于长期出活在暗处。视力不太好。',0000000000),
('05053103',0,'A monster with tremendous power, it destroys enemies with a swing of its axe.\nNetorare is the best！','Battle Ox',2,4,4,1,1700,1000,6,0,'牛头人','有着强大力量的牛怪，斧头一挥能砍倒一切东西。牛头人天下第一！',0000000000),
('94119974',0,'A powerful monster whose two heads attack as one.','Two-Headed King Rex',2,4,10,1,1600,1200,8,0,'双头恐龙王','恐龙族中的强力怪兽，两只头同时攻击。',0000000000),
('88979991',0,'A huge bee with exceptional strength that\'s particularly dangerous in a swarm.','Killer Needle',5,4,9,1,1200,1000,6,0,'杀手蜂','巨大的黄蜂，攻击出乎意料的强，若被群体攻击则非常危险。',0000000000),
('13429800',0,'A giant white shark with razor-sharp teeth.','Great White',3,4,12,1,1600,800,10,0,'大白鲨','巨大的白鲨，若被咬到绝对无法脱身。',0000000000),
('68516705',0,'Half man and half horse, this monster is known for its extreme speed.','Mystic Horseman',2,4,5,1,1300,1550,6,0,'人马兽','人与马化身的怪兽，奔跑的速度谁也追不上。',0000000000),
('02863439',0,'A bird-beast that summons reinforcements with a hand mirror.','Fiend Reflection #2',1,4,6,1,1100,1400,4,0,'幻象鸟','能够从手里的镜子中召唤出同伴的鸟怪。',0000000000),
('31987274',0,'Three wishes are granted to those fortunate enough to see this monster in flight.','Flying Fish',5,4,12,1,800,500,7,0,'飞鱼','据说谁看见它在空中飞翔的姿态，就能实现三个愿望。',0000000000),
('14898066',0,'This wicked Beast-Warrior does every horrid thing imaginable, and loves it! His axe bears the marks of his countless victims.','Vorse Raider',0,4,4,1,1900,1200,3,0,'血腥魔兽人','以做尽坏事而引以为荣的魔兽人，手上的斧头沾满鲜血。',0000000000),
('45909477',0,'Many have fallen victim to this lunar warrior\'s crescent pike.','Moon Envoy',1,4,3,1,1100,1000,4,0,'月之使者','侍奉月之女神的战士。使用像新月一样的戈进行攻击！',0000000000),
('79629370',0,'A sorcerer blessed by lunar light with powers far beyond mortal comprehension.','Maiden of the Moonlight',1,4,0,1,1500,1300,4,0,'月光少女','月亮加护的魔导士。神秘的魔法能使人看到幻觉。',0000000000),
('15303296',0,'A very elusive creature that looks like a harmless statue until it attacks.','Ryu-Kishin',0,4,7,1,1000,500,7,0,'石像怪','使人误认为是石像，从而在黑暗之中攻击。逃跑速度也很快。',0000000000),
('68846917',0,'Protected by a solid body of rock, this monster throws a bone-shattering punch.','Rock ogre Grotto #1',2,3,17,1,800,1200,8,0,'岩窟魔人','岩石之躯成就高强的守备力。挥起巨腕时务必警惕。',0000000000),
('66602787',0,'This clown appears from nowhere and executes very strange moves to avoid enemy attacks.','Saggi the Dark Clown',0,3,0,1,600,1500,1,0,'暗道化师 扎奇','来去无踪的道化师，以不可思议的动作躲避攻击。',0000000000),
('20277860',0,'This warrior blindly swings a deadly blade with devastating force.','Armored Zombie',0,3,2,1,1500,0,3,0,'铠武者僵尸','充满怨念而苏醒的武者，要警惕在黑暗中挥舞的太刀。',0000000000),
('53375573',0,'It\'s said that this King of the Netherworld once had the power to rule over the dark.','Dark King of the Abyss',0,3,7,1,1200,800,1,0,'深渊的冥王','冥界之王，听说以前有着支配所有黑暗力量的能力。',0000000000),
('74637266',0,'With the head of a fish and the legs of an octopus, this strange creature attacks enemies by flinging spears.','Octoberser',3,5,14,1,1600,1400,10,0,'章鱼怪','鱼的头，章鱼的脚，非常不可思议的生物，用枪发动攻击。',0000000000),
('41061625',0,'An intelligent palm tree that drops a hail of rock-hard coconuts on its enemies.','Yashinoki',2,2,18,1,800,600,8,0,'椰树','有意志的椰子树。以落下的果实进行攻击。果实中的椰汁味道鲜美。',0000000000),
('46718686',0,'A rabid starfish that spits a lethal acid that can melt almost anything.','Hitodenchak',3,2,14,1,600,700,10,0,'海星葵','被污染的水源侵蚀而狂暴化的海星。从口中吐出溶解液攻击敌人。',0000000000),
('13179332',0,'A wicked wooden spirit that has burned out. The barbecue grilled with this charcoal is so awesome that everybody thinks it\'s priceless.','Charcoal Inpachi',4,1,15,1,100,2100,5,0,'大木炭18','完全燃烧殆尽的巨木之化身。用此炭烤出的肉被公认为极品而受到珍重。',0000000000),
('76634149',0,'A sea dragon known as the King of the Ocean, it attacks its enemies with huge tidal waves.','Kairyu-Shin',3,5,13,1,1800,1500,10,0,'海龙神','被称为海洋之主的海龙。掀起海啸吞噬一切。',0000000000),
('96851799',0,'An intensely hot flame creature that rams anything standing in its way.','Hinotama Soul',4,2,15,1,600,500,5,0,'史汀','非常炽热的火焰形成的块，并用那身体冲撞敌人。',0000000000),
('14181608',0,'Found in humid regions, this creature attacks enemies with a lethal rain of poison spores.','Mushroom Man',2,2,18,1,800,600,8,0,'蘑菇人','在潮湿的地方发挥力量！从蘑菇帽中吐丝攻击。',0000000000),
('21251800',0,'I am the ruler of all creation sitting atop the supreme throne... let there be light!','Light Bringer Lucifer',0,6,8,1,2600,1800,11,0,'带来光明的使者 路西法','吾乃君临至高玉座统治天地者…要有光！',0000000000),
('69380702',0,'This rabbit\'s got a sweet tooth! He\'s on a quest for the world\'s sweetest carrot, and just wants to nibble carrots all day today, tomorrow, and the day after that!','Bunilla',2,1,5,1,150,2050,6,0,'小白兔','非常喜欢甜食的甜食派兔子。在找据说是世界第一甜的甘糖胡萝卜，无论今天还是明天都想要啃胡萝卜。',0000000000),
('32269855',0,'A proud ruler of the jungle that some fear and others respect.','The All-Seeing White Tiger',5,3,5,1,1300,500,6,0,'独眼白虎','对有些人来说感到恐怖，对有些人来说却是尊敬的对象，孤傲的丛林之王。',0000000000),
('01929294',0,'Enemies\' hearts will melt at the sight of this small fairy\'s cuteness.','Key Mace',1,1,8,1,400,300,2,0,'心钥妖精','非常细小的天使。谁都会因为觉得她十分可爱从而放松警戒。',0000000000),
('34290067',0,'A zombie shark that can deliver its lethal curse with a spell.','Corroding Shark',0,3,2,1,1100,700,3,0,'死亡鲨','在海里彷徨的鲨鱼，遇上它的人都会被施与诅咒。',0000000000),
('21239280',0,'A mouse that has returned as a zombie to seek revenge on cats.','Bone Mouse',0,1,2,1,400,300,3,0,'骨鼠','被猫杀死的老鼠。为了报仇雪恨化为不死形态。',0000000000),
('12470447',1,'','Curse of Fiend',7,0,24,1,0,0,0,0,'邪恶的仪式','场上全部的怪兽表示形式交换，且本回合不可再变更。',0000000000),
('70046172',1,'','Rush Recklessly',7,0,24,1,0,0,0,3,'突进','场上表侧表示存在的1只怪兽的攻击力直到结束阶段时上升700。',0000000013),
('44763025',1,'','Delinquent Duo',7,0,24,1,0,0,0,0,'爱恶作剧的双子恶魔','支付1000基本分发动。对方手卡随机2张丢弃。',0000000000),
('22046459',1,'','Megamorph',7,0,24,1,0,0,0,0,'巨大化','选择一只怪兽，自己基本分比对方低的场合，攻击力变成2倍的数值。自己基本分比对方高的场合，攻击力变成一半的数值。',0000000013),
('12580477',1,'','Raigeki',7,0,24,1,0,0,0,0,'雷击','对方场上存在的怪兽全部破坏。',0000000000),
('59197169',1,'','Yami',7,0,24,1,0,0,0,2,'暗','全场魔法师族·恶魔族怪兽的攻击力·守备力上升50%',0000000000),
('22702055',1,'','Umi',7,0,24,1,0,0,0,2,'海','全场鱼族·海龙族·雷族·水族怪兽的攻击力·守备力上升50%',0000000000),
('86318356',1,'','Sogen',7,0,24,1,0,0,0,2,'草原','全场战士族·兽战士族怪兽的攻击力·守备力上升50%',0000000000),
('50913601',1,'','Mountain',7,0,24,1,0,0,0,2,'山','全场龙族·鸟兽族·雷族怪兽的攻击力·守备力上升50%',0000000000),
('23424603',1,'','Wasteland',7,0,24,1,0,0,0,2,'荒野','全场恐龙族·不死族·岩石族怪兽的攻击力·守备力上升50%',0000000000),
('87430998',1,'','Forest',7,0,24,1,0,0,0,2,'森','全场昆虫族·植物族·兽族·兽战士族怪兽的攻击力·守备力上升50%',0000000000),
('66788016',1,'','Fissure',7,0,24,1,0,0,0,0,'地裂','对方场上表侧表示存在的1只攻击力最低的怪兽破坏。',0000000000),
('97169186',1,'','Smashing Ground',7,0,24,1,0,0,0,0,'地碎','对方场上表侧表示存在的1只守备力最高的怪兽破坏。',0000000000),
('53129443',1,'','Dark Hole',7,0,24,1,0,0,0,0,'黑洞','场上存在的怪兽全部破坏。',0000000000),
('63102017',1,'','Stop Defense',7,0,24,1,0,0,0,0,'「守备」封禁','发动后可选择1只对方场上的怪兽变表侧攻击表示。',0000000009),
('55144522',1,'','Pot of Greed',7,0,24,1,0,0,0,0,'强欲之壶','从自己的卡组抽2张卡。',0000000000),
('83764718',1,'','Monster Reborn',7,0,24,1,0,0,0,0,'死者苏生','发动后可选择自己或对方的墓地1只怪兽特殊召唤到自己场上。',0000000000),
('19613556',1,'','Heavy Storm',7,0,24,1,0,0,0,0,'大风暴','场上存在的魔法·陷阱卡全部破坏。',0000000000),
('25880422',1,'','Block Attack',7,0,24,1,0,0,0,0,'「攻击」封禁','指定的对方场上的1只表侧表示的怪兽转为守备表示。',0000000007),
('52097679',1,'','Shield & Sword',7,0,24,1,0,0,0,0,'右手持盾左手持剑','场上所有表侧表示的怪兽的攻击力与守备力互换。',0000000000),
('01248895',2,'','Chain Destruction',8,0,24,1,0,0,0,0,'连锁破坏','对方的怪兽召唤·反转召唤·特殊召唤成功时触发。若那只怪兽的攻击力在2000以下，那只怪兽与对方手牌，卡组中的同名卡全部破坏。',0000000000),
('22359980',2,'','Mirror Wall',8,0,24,1,0,0,0,0,'银幕之镜壁','对方怪兽攻击时触发。对方场上所有表侧表示的怪兽攻击力变成一半。',0000000000),
('96355986',2,'','Enchanted Javeline',8,0,24,1,0,0,0,0,'神圣标枪','对方怪兽攻击时触发。自己基本分回复那1只攻击怪兽的攻击力的数值。',0000000000),
('04206964',2,'','Trap Hole',8,0,24,1,0,0,0,0,'落穴','对方的怪兽的召唤·反转召唤成功时触发。若那只怪兽攻击力在1000以上，那只怪兽破坏。',0000000000),
('04031928',1,'','Change of Heart',7,0,24,1,0,0,0,0,'心变','发动后可选择对方的场上的1只怪兽并获得控制权。',0000000009),
('54109233',2,'','Infinite Dismissal',8,0,24,1,0,0,0,0,'救世主之蚁地狱','对方的怪兽召唤·反转召唤时触发。若那只怪兽是4星或以下，破坏那只怪兽。',0000000000),
('53582587',2,'','Torrential Tribute',8,0,24,1,0,0,0,0,'激流葬','对方怪兽召唤·反转召唤·特殊召唤时触发。场上存在的怪兽全部破坏。',0000000000),
('44095762',2,'','Mirror Force',8,0,24,1,0,0,0,0,'神圣防护罩-反射镜力-','对方怪兽攻击时触发。对方场上存在的攻击表示怪兽全部破坏。',0000000000),
('65396880',2,'','Huge Revolution',8,0,24,1,0,0,0,0,'大革命','对方怪兽攻击时触发。若我方场上有攻击表示的等级3或以下怪兽，对方场上的卡全部破坏，对方的手牌全部丢弃。否则我方场上的卡全部破坏，我方的手牌全部丢弃。',0000000000);

/*Table structure for table `Deck` */

DROP TABLE IF EXISTS `Deck`;

CREATE TABLE `Deck` (
  `name` varchar(50) NOT NULL,
  `coverpassword` varchar(10) NOT NULL,
  `composition` varchar(1000) NOT NULL,
  `id` int(10) NOT NULL,
  `text` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `Deck` */

insert  into `Deck`(`name`,`coverpassword`,`composition`,`id`,`text`) values 
('海龙神的愤怒','76634149','67371383,86164529,09540040,12146024,13429800,13429800,74637266,46718686,46718686,46718686,76634149,76634149,31987274,31987274,22702055,22702055,22702055,22359980,53582587,53582587,25880422,25880422,63102017,63102017,22046459',1,'以环境魔法卡【海】为特色的套牌，各方面都比较均衡。拥有可怕的必杀卡牌【激流葬】！'),
('黑暗力量的鼓动','53375573','66602787,66602787,66602787,14898066,70781052,46986414,53375573,28563545,15303296,15303296,48649353,87564352,59197169,59197169,59197169,53129443,53129443,53375573,12470447,12470447,44763025,44763025,01248895,01248895,54109233',2,'以环境魔法卡【暗】为特色的套牌，活用各种魔法陷阱来阻挠对手，再使用最上级怪兽一锤定音！'),
('坚毅的战士们','45231177','76232340,55908887,94905343,45231177,45231177,32012841,05053103,05053103,45909477,45909477,45909477,98502113,76184692,76184692,86318356,86318356,86318356,97169186,97169186,52097679,52097679,25880422,63102017,70046172,04206964',3,'以环境魔法卡【草原】为特色的套牌，怪兽与魔法的搭配尤为重要，堂堂正正地决出胜负吧！'),
('丛林深处的秘密','14015067','14015067,69780745,32485271,80141480,88979991,41061625,41061625,14181608,69380702,69380702,32269855,68516705,87430998,87430998,87430998,70046172,70046172,70046172,52097679,52097679,22046459,22046459,04206964,04206964,04206964',4,'以环境魔法卡【森】为特色的套牌，强力魔法卡【巨大化】使得弱小的怪兽也可以瞬间粉碎敌人！'),
('干涸大地的战栗','94119974','94119974,94119974,68171737,75390004,14575467,11250655,44865098,68846917,68846917,20277860,21239280,21239280,21239280,23424603,23424603,23424603,66788016,66788016,66788016,12580477,54109233,54109233,04206964,04206964,04206964',5,'以环境魔法卡【荒野】为特色的套牌，适者生存、优胜劣汰是亘古不变的法则。摧毁对手的怪兽，然后直击获胜！'),
('狂风与业火的制裁','70345785','70345785,70345785,66889139,74677422,78984772,74703140,46696593,87564352,02863439,13179332,13179332,96851799,96851799,96851799,50913601,50913601,19613556,19613556,52684508,63102017,63102017,25880422,25880422,22046459,22046459',6,'强力的怪兽们在环境魔法【山】的加持下如虎添翼，让对手感受被绝对力量支配的恐惧！'),
('汉诺的崇高力量','44095762','21251800,28913279,28546905,35565537,35565537,75850803,75850803,45909477,79629370,79629370,01929294,01929294,01929294,04031928,83764718,83764718,44095762,44095762,44095762,96355986,96355986,96355986,22359980,22359980,22359980',7,'神圣的精灵们所向披靡，臣服在汉诺崇高的力量面前吧！'),
('君临顶点的传说','89631139','89631139,89631139,89631139,91998119,32012841,32012841,32012841,74677422,74677422,74677422,54752875,17655904,17655904,52684508,52684508,55144522,55144522,55144522,83764718,83764718,83764718,04031928,44095762,53582587,53582587',8,'你渴望力量吗？（此套牌的强度远高于其它预组，请尽量避免选用）');

/*Table structure for table `Enum` */

DROP TABLE IF EXISTS `Enum`;

CREATE TABLE `Enum` (
  `name` varchar(50) NOT NULL,
  `value` int(11) NOT NULL,
  `category` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `Enum` */

insert  into `Enum`(`name`,`value`,`category`) values 
('Aqua',14,'MonsterType'),
('Beast',5,'MonsterType'),
('BeastWarrior',4,'MonsterType'),
('Continuous',5,'Icon'),
('Counter',6,'Icon'),
('Cyberse',23,'MonsterType'),
('Dark',0,'Attribute'),
('Dinosaur',10,'MonsterType'),
('Divine',6,'Attribute'),
('DivineBeast',21,'MonsterType'),
('Dragon',1,'MonsterType'),
('Earth',2,'Attribute'),
('Equip',1,'Icon'),
('Fairy',8,'MonsterType'),
('Field',2,'Icon'),
('Fiend',7,'MonsterType'),
('Fire',4,'Attribute'),
('Fish',12,'MonsterType'),
('Insect',9,'MonsterType'),
('Light',1,'Attribute'),
('Machine',19,'MonsterType'),
('Normal',0,'Icon'),
('Plant',18,'MonsterType'),
('Psychic',20,'MonsterType'),
('Pyro',15,'MonsterType'),
('QuickPlay',3,'Icon'),
('Reptile',11,'MonsterType'),
('Ritual',4,'Icon'),
('Rock',17,'MonsterType'),
('SeaSerpent',13,'MonsterType'),
('Spellcaster',0,'MonsterType'),
('Thunder',16,'MonsterType'),
('Warrior',3,'MonsterType'),
('Water',3,'Attribute'),
('Wind',5,'Attribute'),
('WingedBeast',6,'MonsterType'),
('Wyrm',22,'MonsterType'),
('Zombie',2,'MonsterType'),
('Normal',1,'CardType'),
('Effect',2,'CardType'),
('Ritual',4,'CardType'),
('Fusion',8,'CardType'),
('Synchro',16,'CardType'),
('Xyz',32,'CardType'),
('Toon',64,'CardType'),
('Spirit',128,'CardType'),
('Union',256,'CardType'),
('Gemini',512,'CardType'),
('Tuner',1024,'CardType'),
('Flip',2048,'CardType'),
('Pendulum',4096,'CardType'),
('Link',8192,'CardType'),
('None',0,'SummonedAttribute'),
('Dark',1,'SummonedAttribute'),
('White',2,'SummonedAttribute'),
('Devil',3,'SummonedAttribute'),
('Fantasy',4,'SummonedAttribute'),
('Fire',5,'SummonedAttribute'),
('Forest',6,'SummonedAttribute'),
('Wind',7,'SummonedAttribute'),
('Earth',8,'SummonedAttribute'),
('Thunder',9,'SummonedAttribute'),
('Water',10,'SummonedAttribute'),
('Divine',11,'SummonedAttribute'),
('Monster',0,'CardCategory'),
('Spell',1,'CardCategory'),
('Trap',2,'CardCategory');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
