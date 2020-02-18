using System;
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
            EffectWhenSelfLeave = new Dictionary<string, (MethodInfo methodInfo, string targetID)>();
            TrapsWhenSummon = new Queue<SpellAndTrapCard>();
            TrapsWhenAttack = new Queue<SpellAndTrapCard>();
        }

        public void DrawPhase()
        {
            YourTurn = true;
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
                t.methodInfo.Invoke(null, new object[] { null, t.targetID, this, Enemy });
            }
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
                    Lose = true;
                    return;
                }
                Card card = Deck[Deck.Count - 1];
                Deck.Remove(card);
                Hands.Add(card);
            }
        }

        public void DiscardHands(int count = 1)
        {
            while (Hands.Count > 0 && count-- > 0)
            {
                Card card = Hands[Hands.Count - 1];
                Hands.Remove(card);
                Grave.Add(card);
            }
        }

        public void IncreaseHP(int point)
        {
            HP += point;
            if (HP <= 0)
            {
                Lose = true;
            }
        }

        public void DecreaseHP(int point)
        {
            IncreaseHP(-point);
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

        public void EffectFromHands(SpellAndTrapCard card, int fieldIndex)
        {
            Hands.Remove(card);
            Field.SpellAndTrapFields[fieldIndex] = card;

            EffectFromField(fieldIndex);
        }

        public void EffectFromField(int fieldIndex)
        {
            var card = fieldIndex == 5 ? Field.FieldField : Field.SpellAndTrapFields[fieldIndex];
            card.Status.FaceDown = false;
            EffectingCard = card;
            Type type = Type.GetType("YuGiOh2.Cards.C" + card.Password);
            MethodInfo methodInfo = type.GetMethod("CheckIfAvailable");
            bool availbale = (bool)methodInfo.Invoke(null, new object[] { card, this, Enemy });
            if (availbale)
            {
                PropertyInfo propertyInfo = type.GetProperty("Type");
                ChooseTarget = (int)propertyInfo.GetValue(null);
            }
            else
            {
                ChooseTarget = 0;
            }
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

        private void SetField(SpellAndTrapCard card)
        {
            if (Field.FieldField != null)
            {
                if (Field.FieldField.Status.FaceDown)
                {
                    Grave.Add(Field.FieldField);
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
            if (Field.FieldField != null)
            {
                if (Field.FieldField.Status.FaceDown)
                {
                    Grave.Add(Field.FieldField);
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
            DuelUtils.ResetCard(tmp);
            Enemy.Grave.Add(tmp);
            Enemy.Field.FieldField = null;
            Enemy.ProcessEffectWhenLeave(tmp.UID);
            Enemy.EffectWhenSummon.Remove(tmp.UID);
        }

        public void ProcessEffect(string targetID)
        {
            ChooseTarget = 0;
            Type type = Type.GetType("YuGiOh2.Cards.C" + EffectingCard.Password);

            MethodInfo methodInfo;
            if (EffectingCard.Icon == 2)
            {
                methodInfo = type.GetMethod("ProcessWhenSummon");
                EffectWhenSummon.Add(EffectingCard.UID, (methodInfo, null));
                methodInfo = type.GetMethod("ProcessWhenLeave");
                EffectWhenSelfLeave.Add(EffectingCard.UID, (methodInfo, null));
                EffectingCard = null;
                ProcessEnemyField();
                methodInfo = type.GetMethod("ProcessEffect");
                methodInfo.Invoke(null, new object[] { EffectingCard, targetID, this, Enemy });
                return;
            }
            methodInfo = type.GetMethod("ProcessEffect");
            methodInfo.Invoke(null, new object[] { EffectingCard, targetID, this, Enemy });

            for (int i = 0; i < 5; i++)
            {
                if (Field.SpellAndTrapFields[i] == null)
                    continue;

                if (Field.SpellAndTrapFields[i].UID == EffectingCard.UID)
                {
                    Grave.Add(Field.SpellAndTrapFields[i]);
                    Field.SpellAndTrapFields[i] = null;
                }
            }
            methodInfo = type.GetMethod("ProcessEndPhase");
            if (methodInfo != null)
            {
                EffectTillEndPhase.Enqueue((methodInfo, targetID));
            }
            EffectingCard = null;
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

        private void SetMonster(MonsterCard card)
        {
            int[] sort = new int[] { 2, 1, 3, 0, 4 };
            foreach (int num in sort)
            {
                if (Field.MonsterFields[num] == null)
                {
                    card.Status.DefensePosition = true;
                    card.Status.FaceDown = true;
                    Hands.Remove(card);
                    Field.MonsterFields[num] = card;
                    CanSummon = false;
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
            Enemy.DecreaseHP(card.ATK);
            card.Status.AttackChances--;
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
            }
        }

        public void ProcessContinuousEffectWhenSummon(string cardID)
        {
            foreach (var item in EffectWhenSummon.Values)
            {
                item.methodInfo.Invoke(null, new object[] { null, cardID, this, Enemy });
            }
        }

        public void CheckTrapsWhenSummon()
        {
            if (Enemy.TrapsWhenSummon.Count > 0)
            {
                var trap = Enemy.TrapsWhenSummon.Dequeue();
                trap.Status.FaceDown = false;
                Enemy.EffectingCard = trap;
            }
        }

        public void ProcessEffectWhenLeave(string cardID)
        {
            if (!EffectWhenSelfLeave.ContainsKey(cardID))
                return;

            EffectWhenSelfLeave[cardID].methodInfo
                .Invoke(null, new object[] { null, EffectWhenSelfLeave[cardID].targetID, this, Enemy });
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
                if (!target.Status.DefensePosition)
                {
                    Enemy.DecreaseHP(attackerPoint - targetPoint);
                }
                DuelUtils.ResetCard(target);
                Enemy.Grave.Add(target);
                Enemy.Field.MonsterFields[targetIndex] = null;
                attacker.Status.AttackChances--;
                attacker.Status.CanChangePosition = false;
            }
            else if (attackerPoint < targetPoint)
            {
                if (!target.Status.DefensePosition)
                {
                    DuelUtils.ResetCard(attacker);
                    Grave.Add(attacker);
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
                    DuelUtils.ResetCard(attacker);
                    Grave.Add(attacker);
                    Field.MonsterFields[attackerIndex] = null;
                    DuelUtils.ResetCard(target);
                    Enemy.Grave.Add(target);
                    Enemy.Field.MonsterFields[targetIndex] = null;
                }
                else
                {
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
