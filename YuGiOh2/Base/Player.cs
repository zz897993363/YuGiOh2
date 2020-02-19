﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YuGiOh2.Data;

namespace YuGiOh2.Base
{
    public class Player
    {
        public string ID { get; set; }
        public int HP { get; set; }
        public Field Field { get; set; }
        public List<Card> Grave { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> Hands { get; set; }
        public Player Enemy { get; set; }
        public bool CanSummon { get; set; }
        public bool Lose { get; set; }
        public bool YourTurn { get; set; }
        public bool FirstTurn { get; set; }
        public string Message { get; set; } = "";
        /// <summary>
        /// 取对象的范围
        /// </summary>
        public int ChooseTarget { get; set; }
        /// <summary>
        /// 正在发动中的卡牌
        /// </summary>
        public SpellAndTrapCard EffectingCard { get; set; }
        /// <summary>
        /// 处理当回合有效的效果结算
        /// </summary>
        public Queue<(MethodInfo methodInfo, string targetID)> EffectTillEndPhase { get; set; }
        /// <summary>
        /// 处理召唤时结算的永续效果
        /// </summary>
        public Dictionary<string, (MethodInfo methodInfo, string targetID)> EffectWhenSummon { get; set; }
        /// <summary>
        /// 处理盖怪时结算的永续效果
        /// </summary>
        public Dictionary<string, (MethodInfo methodInfo, string targetID)> EffectWhenSetMonster { get; set; }
        /// <summary>
        /// 处理攻击时结算的永续效果
        /// </summary>
        public Dictionary<string, (MethodInfo methodInfo, string targetID)> EffectWhenAttack { get; set; }
        /// <summary>
        /// 自身离场时结算的效果
        /// </summary>
        public Dictionary<string, (MethodInfo methodInfo, string targetID)> EffectWhenSelfLeave { get; set; }
        /// <summary>
        /// 在召唤时触发的陷阱
        /// </summary>
        public Queue<SpellAndTrapCard> TrapsWhenSummon { get; set; }
        /// <summary>
        /// 在攻击时触发的陷阱
        /// </summary>
        public Queue<SpellAndTrapCard> TrapsWhenAttack { get; set; }

        public Player(ICollection<Card> cards = null, string id = null, int hp = 8000)
        {
            ID = id;
            HP = hp;
            Field = new Field();
            Grave = new List<Card>();
            Deck = new List<Card>(cards ?? new Card[] { });
            Hands = new List<Card>();
            EffectTillEndPhase = new Queue<(MethodInfo methodInfo, string targetID)>();
            EffectWhenAttack = new Dictionary<string, (MethodInfo methodInfo, string targetID)>();
            EffectWhenSummon = new Dictionary<string, (MethodInfo methodInfo, string targetID)>();
            EffectWhenSetMonster = new Dictionary<string, (MethodInfo methodInfo, string targetID)>();
            EffectWhenSelfLeave = new Dictionary<string, (MethodInfo methodInfo, string targetID)>();
            TrapsWhenSummon = new Queue<SpellAndTrapCard>();
            TrapsWhenAttack = new Queue<SpellAndTrapCard>();
        }

        public void AddCardToGrave(ref Card card)
        {
            DuelUtils.ResetCard(ref card);
            ToGrave(card);
        }

        public void AddCardToGrave(ref MonsterCard card)
        {
            DuelUtils.ResetCard(ref card);
            ToGrave(card);
        }

        public void AddCardToGrave(ref SpellAndTrapCard card)
        {
            DuelUtils.ResetCard(ref card);
            ToGrave(card);
        }

        private void ToGrave(Card card)
        {
            Grave.Add(card);
            Message += $"你的【{card.Cname}】被送入墓地\r\n";
            Enemy.Message += $"对手的【{card.Cname}】被送入墓地\r\n";
        }

        public void DrawPhase()
        {
            YourTurn = true;
            Message += $"你的抽牌阶段（生命值：{HP}）\r\n";
            Enemy.Message += $"对手的抽牌阶段（生命值：{HP}）\r\n";
            while (Hands.Count < 5)
            {
                DrawCard();
            }
            CanSummon = true;
        }

        public void StandByPhase()
        {
            Field.MonsterFields
                .Where(card => card != null)
                .ToList()
                .ForEach(card =>
                {
                    card.Status.AttackChances = 1;
                    card.Status.CanChangePosition = true;
                });
        }

        public void EndPhase()
        {
            while (EffectTillEndPhase.Count > 0)
            {
                var t = EffectTillEndPhase.Dequeue();
                t.methodInfo.Invoke(null, new object[] { t.targetID, this });
            }
            Message += $"你结束了回合（生命值：{HP}）\r\n";
            Enemy.Message += $"对手结束了回合（生命值：{HP}）\r\n";
            while (Hands.Count > 5)
            {
                DiscardHands();
            }
            YourTurn = false;
            FirstTurn = false;
        }

        public void DrawCard(int count = 1)
        {
            while (count-- > 0)
            {
                if (Deck.Count == 0)
                {
                    Message += "你已无牌可抽!\r\n";
                    Enemy.Message += "对手已无牌可抽!\r\n";
                    Lose = true;
                    return;
                }
                Card card = Deck[Deck.Count - 1];
                Deck.Remove(card);
                Hands.Add(card);
                Message += $"你抽到了【{card.Cname}】\r\n";
                Enemy.Message += "对手抽了一张牌\r\n";
            }
        }

        public void DiscardHands(int count = 1)
        {
            while (Hands.Count > 0 && count-- > 0)
            {
                Card card = Hands[Hands.Count - 1];
                Message += $"你丢弃了【{card.Cname}】\r\n";
                Enemy.Message += "对手丢弃了【{card.Cname}】\r\n";
                Hands.Remove(card);
                AddCardToGrave(ref card);
            }
        }

        public void IncreaseHP(int point)
        {
            HP += point;
            Message += $"你{(point > 0 ? "回复" : "减少")}了{Math.Abs(point)}点生命值\r\n";
            Message += $"你的生命值：{HP}\r\n";
            Enemy.Message += $"对手{(point > 0 ? "回复" : "减少")}了{Math.Abs(point)}点生命值\r\n";
            Enemy.Message += $"对手的生命值：{HP}\r\n";
            if (HP <= 0)
            {
                Lose = true;
            }
        }

        public void DecreaseHP(int point)
        {
            IncreaseHP(-point);
        }

        public void SummonMonsterFromHands(string UID)
        {
            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (int num in sort)
            {
                if (Field.MonsterFields[num] == null)
                {
                    SummonMonsterFromHands(UID, num);
                    break;
                }
            }
        }

        public void SummonMonsterFromHands(string UID, int fieldIndex)
        {
            if (!CanSummon)
                return;
            MonsterCard card = Hands.FirstOrDefault(c => c.UID == UID) as MonsterCard;
            if (!FirstTurn)
            {
                card.Status.AttackChances = 1;
            }
            Hands.Remove(card);
            if (Field.MonsterFields[fieldIndex] != null)
                Fusion(card, fieldIndex);
            else
                Field.MonsterFields[fieldIndex] = card;
            CanSummon = false;
            Message += $"你召唤了【{card.Cname}】\r\n";
            Enemy.Message += $"对手召唤了【{card.Cname}】\r\n";
        }

        public void EffectFromHands(string UID)
        {
            SpellAndTrapCard card = Hands.FirstOrDefault(c => c.UID == UID) as SpellAndTrapCard;
            if (card.Icon == 2)
            {
                EffectFieldFromHands(card);
                return;
            }
            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (int num in sort)
            {
                if (Field.SpellAndTrapFields[num] == null)
                {
                    EffectFromHands(card, num);
                    break;
                }
            }
        }

        private void EffectFromHands(SpellAndTrapCard card, int fieldIndex)
        {
            Hands.Remove(card);
            Field.SpellAndTrapFields[fieldIndex] = card;

            Message += $"你从手中发动【{card.Cname}】\r\n";
            Enemy.Message += $"对手从手中发动【{card.Cname}】\r\n";
            EffectFromField(fieldIndex);
        }

        public void EffectFromField(int fieldIndex)
        {
            var card = fieldIndex == 5 ? Field.FieldField : Field.SpellAndTrapFields[fieldIndex];
            card.Status.FaceDown = false;
            EffectingCard = card;
            Message += $"你场上的【{card.Cname}】效果发动！\r\n";
            Enemy.Message += $"对手场上的【{card.Cname}】效果发动！\r\n";
            Type type = Type.GetType("YuGiOh2.Cards.C" + card.Password);
            MethodInfo methodInfo = type.GetMethod("CheckIfAvailable");
            bool availbale = (bool)methodInfo.Invoke(null, new object[] { this });
            if (availbale)
            {
                PropertyInfo propertyInfo = type.GetProperty("Type");
                ChooseTarget = (int)propertyInfo.GetValue(null);
            }
            else
            {
                ChooseTarget = 0;
                Message += $"你的【{card.Cname}】发动时机不正确，没有效果！\r\n";
                Enemy.Message += $"对手的【{card.Cname}】发动时机不正确，没有效果！\r\n";
            }
        }

        private void SetField(SpellAndTrapCard card)
        {
            Message += $"你盖放了场地魔法卡【{card.Cname}】\r\n";
            Enemy.Message += "对手盖放了一张场地魔法卡\r\n";
            if (Field.FieldField != null)
            {
                if (Field.FieldField.Status.FaceDown)
                {
                    AddCardToGrave(ref Field.FieldField);
                }
                else
                {
                    Enemy.ProcessEnemyField();
                }
            }
            Hands.Remove(card);
            Field.FieldField = card;
            card.Status.FaceDown = true;
        }

        private void EffectFieldFromHands(SpellAndTrapCard card)
        {
            Message += $"你发动了场地魔法卡：{card.Cname}\r\n";
            Enemy.Message += $"对手发动了场地魔法卡：{card.Cname}\r\n";
            if (Field.FieldField != null)
            {
                if (Field.FieldField.Status.FaceDown)
                {
                    AddCardToGrave(ref Field.FieldField);
                }
                else
                {
                    Enemy.ProcessEnemyField();
                }
            }
            Hands.Remove(card);
            Field.FieldField = card;
            EffectingCard = card;
            ProcessEnemyField();
        }

        public void ProcessEnemyField()
        {
            if (Enemy.Field.FieldField == null)
                return;

            var tmp = Enemy.Field.FieldField;
            Enemy.AddCardToGrave(ref tmp);
            Enemy.Field.FieldField = null;
            Enemy.ProcessEffectWhenLeave(tmp.UID);
            Enemy.EffectWhenSummon.Remove(tmp.UID);
            Enemy.EffectWhenSetMonster.Remove(tmp.UID);
            Enemy.EffectWhenAttack.Remove(tmp.UID);
            Enemy.EffectWhenSelfLeave.Remove(tmp.UID);
        }

        public void ProcessEffect(string targetID)
        {
            Type type = Type.GetType("YuGiOh2.Cards.C" + EffectingCard.Password);
            MethodInfo methodInfo;
            if (EffectingCard.Icon == 2)
            {
                methodInfo = type.GetMethod("ProcessWhenSummon");
                EffectWhenSummon.Add(EffectingCard.UID, (methodInfo, null));
                methodInfo = type.GetMethod("ProcessWhenSetMonster");
                EffectWhenSetMonster.Add(EffectingCard.UID, (methodInfo, null));
                methodInfo = type.GetMethod("ProcessWhenLeave");
                EffectWhenSelfLeave.Add(EffectingCard.UID, (methodInfo, null));
                EffectingCard = null;
                ProcessEnemyField();
                methodInfo = type.GetMethod("ProcessEffect");
                methodInfo.Invoke(null, new object[] { this });
                return;
            }
            methodInfo = type.GetMethod("ProcessEffect");
            if (ChooseTarget == 0 && EffectingCard.CardCategory == 1)
            {
                methodInfo.Invoke(null, new object[] { this });
            }
            else
            {
                methodInfo.Invoke(null, new object[] { targetID, this });
            }

            for (int i = 0; i < 5; i++)
            {
                if (Field.SpellAndTrapFields[i] == null)
                    continue;

                if (Field.SpellAndTrapFields[i].UID == EffectingCard.UID)
                {
                    AddCardToGrave(ref Field.SpellAndTrapFields[i]);
                    Field.SpellAndTrapFields[i] = null;
                }
            }
            methodInfo = type.GetMethod("ProcessEndPhase");
            if (methodInfo != null)
            {
                EffectTillEndPhase.Enqueue((methodInfo, targetID));
            }
            EffectingCard = null;
            ChooseTarget = 0;
        }

        public void SetFromHands(string UID)
        {
            Card card = Hands.FirstOrDefault(c => c.UID == UID);
            if (card.CardCategory == 0)
            {
                SetMonster(card as MonsterCard);
            }
            else
            {
                SetSpellAndTrap(card as SpellAndTrapCard);
            }
        }

        private void SetMonster(MonsterCard card)
        {
            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (int num in sort)
            {
                if (Field.MonsterFields[num] == null)
                {
                    Message += $"你盖放了【{card.Cname}】\r\n";
                    Enemy.Message += "对手盖放了一只怪兽\r\n";
                    card.Status.DefensePosition = true;
                    card.Status.FaceDown = true;
                    Hands.Remove(card);
                    Field.MonsterFields[num] = card;
                    CanSummon = false;
                    ProcessContinuousEffectWhenSetMonster(card.UID);
                    Enemy.ProcessContinuousEffectWhenSetMonster(card.UID);
                    break;
                }
            }
        }

        private void SetSpellAndTrap(SpellAndTrapCard card)
        {
            if (card.Icon == 2)
            {
                SetField(card);
                return;
            }
            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (int num in sort)
            {
                if (Field.SpellAndTrapFields[num] == null)
                {
                    card.Status.FaceDown = true;
                    Hands.Remove(card);
                    Field.SpellAndTrapFields[num] = card;
                    Message += $"你盖放了【{card.Cname}】\r\n";
                    Enemy.Message += "对手盖放了一张魔法/陷阱卡\r\n";
                    if (card.CardCategory == 2)
                    {
                        Type type = Type.GetType("YuGiOh2.Cards.C" + card.Password);
                        PropertyInfo propertyInfo = type.GetProperty("Type");
                        int mType = (int)propertyInfo.GetValue(null);
                        if (mType == (int)AffectMomentType.WhenAttacked)
                        {
                            TrapsWhenAttack.Enqueue(card);
                        }
                        else if (mType == (int)AffectMomentType.WhenSummoned)
                        {
                            TrapsWhenSummon.Enqueue(card);
                        }
                    }
                    break;
                }
            }
        }

        public void ChangePosition(MonsterCard card)
        {
            if (!card.Status.CanChangePosition)
                return;

            card.Status.FaceDown = false;
            card.Status.DefensePosition = !card.Status.DefensePosition;
            card.Status.CanChangePosition = false;
            Message += $"你的【{card.Cname}】变为了{(card.Status.DefensePosition ? "守备" : "攻击")}表示\r\n";
            Enemy.Message += $"对手的【{card.Cname}】变为了{(card.Status.DefensePosition ? "守备" : "攻击")}表示\r\n";
        }

        public void DirectAttack(int attackerIndex)
        {
            MonsterCard card = Field.MonsterFields[attackerIndex];
            if (card == null)
                return;
            if (card.Status.AttackChances < 1)
                return;
            if (card.Status.DefensePosition)
                return;
            Message += $"你的【{card.Cname}】直接攻击！\r\n";
            Enemy.Message += $"对手的【{card.Cname}】直接攻击！\r\n";
            Enemy.DecreaseHP(card.ATK);
            card.Status.AttackChances--;
            card.Status.CanChangePosition = false;
        }

        public void ProcessContinuousEffectWhenAttack(string cardID)
        {

        }

        public void CheckTrapsWhenAttack()
        {
            if (Enemy.TrapsWhenAttack.Count > 0)
            {
                var trap = Enemy.TrapsWhenAttack.Dequeue();
                trap.Status.FaceDown = false;
                Enemy.EffectingCard = trap;
                Message += $"对手的陷阱卡【{trap.Cname}】触发！\r\n";
                Enemy.Message += $"你的陷阱卡【{trap.Cname}】触发！\r\n";
            }
        }

        public void ProcessContinuousEffectWhenSummon(string cardID)
        {
            foreach (var item in EffectWhenSummon.Values)
            {
                item.methodInfo.Invoke(null, new object[] { cardID, this });
            }
        }

        public void ProcessContinuousEffectWhenSetMonster(string cardID)
        {
            foreach (var item in EffectWhenSetMonster.Values)
            {
                item.methodInfo.Invoke(null, new object[] { cardID, this });
            }
        }

        public void CheckTrapsWhenSummon()
        {
            if (Enemy.TrapsWhenSummon.Count > 0)
            {
                var trap = Enemy.TrapsWhenSummon.Dequeue();
                trap.Status.FaceDown = false;
                Enemy.EffectingCard = trap;
                Message += $"对手的陷阱卡【{trap.Cname}】触发！\r\n";
                Enemy.Message += $"你的陷阱卡【{trap.Cname}】触发！\r\n";
            }
        }

        public void ProcessEffectWhenLeave(string cardID)
        {
            if (!EffectWhenSelfLeave.ContainsKey(cardID))
                return;

            EffectWhenSelfLeave[cardID].methodInfo
                .Invoke(null, new object[] { EffectWhenSelfLeave[cardID].targetID, this });
            EffectWhenSelfLeave.Remove(cardID);
        }

        public void Battle(int attackerIndex, int targetIndex)
        {
            MonsterCard attacker = Field.MonsterFields[attackerIndex];
            if (attacker == null)
                return;
            if (attacker.Status.AttackChances < 1)
                return;
            if (attacker.Status.DefensePosition)
                return;
            MonsterCard target = Enemy.Field.MonsterFields[targetIndex];
            Message += $"你的【{attacker.Cname}】攻击对手的{(target.Status.FaceDown ? "覆盖的怪兽" : "【" + target.Cname + "】")}\r\n";
            Enemy.Message += $"对手的【{attacker.Cname}】攻击你的【{target.Cname}】\r\n";
            int attackerPoint = attacker.ATK;
            int targetPoint = target.Status.DefensePosition ? target.DEF : target.ATK;
            int adv = 0, disadv = 0;
            if (attacker.SummonedAttribute >= (int)SummonedAttribute.Dark &&
                attacker.SummonedAttribute <= (int)SummonedAttribute.Fantasy)
            {
                adv = (attacker.SummonedAttribute + 1) > 4 ? 1 : (attacker.SummonedAttribute + 1);
                disadv = (attacker.SummonedAttribute - 1) < 1 ? 4 : (attacker.SummonedAttribute - 1);
            }
            else if (attacker.SummonedAttribute >= (int)SummonedAttribute.Fire &&
                attacker.SummonedAttribute <= (int)SummonedAttribute.Water)
            {
                adv = (attacker.SummonedAttribute + 1) > 10 ? 5 : (attacker.SummonedAttribute + 1);
                disadv = (attacker.SummonedAttribute - 1) < 5 ? 10 : (attacker.SummonedAttribute - 1);
            }
            if (target.SummonedAttribute == adv)
            {
                attackerPoint += attacker.Level * 100;
            }
            else if (target.SummonedAttribute == disadv)
            {
                targetPoint += target.Level * 100;
            }
            if (attackerPoint > targetPoint)
            {
                Message += $"你的【{attacker.Cname}】战斗破坏了对手的【{target.Cname}】\r\n";
                Enemy.Message += $"对手的【{attacker.Cname}】战斗破坏了你的【{target.Cname}】\r\n";
                if (!target.Status.DefensePosition)
                {
                    Enemy.DecreaseHP(attackerPoint - targetPoint);
                }
                Enemy.AddCardToGrave(ref target);
                Enemy.Field.MonsterFields[targetIndex] = null;
                attacker.Status.AttackChances--;
                attacker.Status.CanChangePosition = false;
            }
            else if (attackerPoint < targetPoint)
            {
                if (!target.Status.DefensePosition)
                {
                    Message += $"你的【{attacker.Cname}】被对手的【{target.Cname}】战斗破坏\r\n";
                    Enemy.Message += $"对手的【{attacker.Cname}】被你的【{target.Cname}】战斗破坏\r\n";
                    AddCardToGrave(ref attacker);
                    Field.MonsterFields[attackerIndex] = null;
                }
                else
                {
                    attacker.Status.AttackChances--;
                    attacker.Status.CanChangePosition = false;
                    target.Status.FaceDown = false;
                }
                DecreaseHP(targetPoint - attackerPoint);
            }
            else
            {
                if (!target.Status.DefensePosition)
                {
                    Message += $"你的【{attacker.Cname}】与对手的【{target.Cname}】同时被战斗破坏\r\n";
                    Enemy.Message += $"对手的【{attacker.Cname}】与你的【{target.Cname}】同时被战斗破坏\r\n";
                    AddCardToGrave(ref attacker);
                    Field.MonsterFields[attackerIndex] = null;
                    Enemy.AddCardToGrave(ref target);
                    Enemy.Field.MonsterFields[targetIndex] = null;
                }
                else
                {
                    Message += $"你的【{attacker.Cname}】与对手的【{target.Cname}】均未被破坏\r\n";
                    Enemy.Message += $"对手的【{attacker.Cname}】与你的【{target.Cname}】均未被破坏\r\n";
                    attacker.Status.AttackChances--;
                    attacker.Status.CanChangePosition = false;
                    target.Status.FaceDown = false;
                }
            }
        }

        public void Fusion(Card card1, int fieldIndex)
        {
            var card_m = DuelUtils.GetFusionCard(card1.Password, Field.MonsterFields[fieldIndex].Password);
            Field.MonsterFields[fieldIndex] = card_m == null ? (MonsterCard)card1 : new MonsterCard(card_m);
        }
    }
}
