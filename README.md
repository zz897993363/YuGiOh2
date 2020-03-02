# YuGiOh2
<h2>这是什么？</h2>
<p>这是一款个人的情怀向作品，是以GB/GBC/GBA/PS等平台上的早期游戏王电子游戏的规则为基础，经过一些个人改编制作而成的在线卡牌对战游戏。
本作品本质上属于一个菜鸟码农自娱自乐的产物，目前仍处于测试阶段，可能会存在各种各样的BUG，如遇见请向作者反馈（QQ：897993363）。
游戏中所使用的图片素材均取自互联网。</p>
<h2>本地启动</h2>
<p>将代码克隆到本地后使用Visual Studio 2019打开工程，初次调试将自动还原依赖。需要事先安装.net core SDK 3.0以及node v10.16.2。
  在MySQL中执行yugioh2db.sql创建数据库，自行替换appsettings.json中的数据库链接字符串</p>
<h2>如何游玩？</h2>
<p>目前仅开放了随机匹配功能，点击【随机匹配】后，从系统预设的8套牌中选取1套你喜欢的套牌，或选择随机套牌，点准备后即进入匹配队列，
由系统匹配对手进行游戏。（由于没什么人，所以实际上和约战也没区别orz）
预计近期内将开放人机对战功能。
有生之年将开放自组卡组功能以及排位模式。（如果真的会有人。。。）</p>
<h2>游戏规则</h2>
<p><strong>胜利条件</strong> 使对方HP归0或者无牌可抽</p>
<p>本游戏的规则大体上参照了GBC游戏《遊戯王デュエルモンスターズII 闇界決闘記》（简称游戏王2）和GBA游戏《Yu-Gi-Oh! - Reshef of Destruction》
（简称游戏王8），与大多数玩家所熟知的OCG/TCG规则相去甚远，但本人有考虑在后续的更新中在控制游戏整体复杂度不提高太多的情况下向OCG规则靠拢。
此外KONAMI推出的游戏王全新规则RUSH DUEL将是重点的关注对象。</p>

<p><strong>关于回合流程</strong> 游戏中回合开始时玩家将抽牌至手牌中拥有5张卡牌为止，游戏中不区分主要流程与战斗流程，这意味着你可以在场上的怪兽攻击后再召唤新的怪兽继续攻击，亦或是在两次攻击中穿插使用任何魔法卡。</p>

<p><strong>关于怪兽卡</strong> 游戏中每回合玩家可召唤一只手中的怪兽卡。召唤怪兽无需解放，等级仅代表其地位。除了OCG中的属性外，本游戏还保留了早期电子游戏中的【召唤魔族】属性，该属性将在怪兽战斗时发挥克制作用，有利的怪兽的数值会在战斗时上升等级*100（原版游戏中则是不计算数值直接秒杀）。
具体的克制链为:【黑】克制【白】克制【恶】克制【幻】克制【黑】;【炎】克制【森】克制【风】克制【土】克制【雷】克制【水】克制【炎】；【神】没有克制关系。目前游戏中的所有怪兽均为通常怪兽，少量怪兽被描述为效果怪兽，但并未实装效果，预计将在之后的更新中进行修正。</p>

<p><strong>关于魔法卡</strong> 游戏中的魔法卡没有发动限制，只要场地上有空的区域，即使不满足发动条件亦可以发动，发动后将不会产生效果。由于不存在OCG中的时点与连锁的概念，也没有回合外操作的机制，除了场地魔法卡外目前游戏中的所有魔法卡均为通常魔法卡。此外部分卡片的效果与OCG中的规则并不一致，需要注意。部分卡片存在效果描述不够准确的请以实际游戏为准，将在后续更新中逐步完善卡片描述。</p>

<p><strong>关于陷阱卡</strong> 游戏中的陷阱卡需要盖放至场上后才能使用。但是为了避免回合外操作带来的复杂处理，本游戏中的陷阱卡为自动触发。
触发的时机目前均为对手召唤怪兽或对手的怪兽攻击时，多个覆盖的陷阱在同一时刻满足触发条件时，优先触发最先覆盖的陷阱，其余陷阱不触发。后续可能会更新
更多在其它时机触发的陷阱，但是自动触发机制短期内不会做调整。卡片效果与描述参见魔法卡部分。</p>
<h2>在线地址</h2>
<p>http://139.196.91.78 渣服务器，初次加载图片资源时间较长还请耐心等待。如需对战可使用不同内核浏览器双开进行左右互搏，人机对战功能预计半个月内上线。</p>