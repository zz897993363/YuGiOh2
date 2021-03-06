﻿using System;
using MoonSharp.Interpreter;
using YuGiOh2.Data;

namespace YuGiOh2.Base
{
    [MoonSharpUserData]
    public abstract class Card
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Cname { get; set; }
        public int CardCategory { get; set; }
        public string CardText { get; set; }
        public string Password { get; set; }
        public int ChooseTargetType { get; set; }
    }

    [MoonSharpUserData]
    public class CardStatus
    {
        public bool FaceDown { get; set; }
        public bool DefensePosition { get; set; }
        public bool CanChangePosition { get; set; }
        public bool Destroyed { get; set; }
        public bool Negated { get; set; }
        public int AttackChances { get; set; }
    }

    //public interface ICardAction
    //{
    //    public abstract void ComeIntoField();
    //    public abstract void ComeIntoGrave();
    //    public abstract void Destroyed();
    //    public abstract void Banished();
    //    public abstract void Sacrificed();
    //}

    [MoonSharpUserData]
    public class MonsterCard : Card//, ICardAction
    {
        public int Attribute { get; set; }
        public int Level { get; set; }
        public int MonsterType { get; set; }
        public int CardType { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int SummonedAttribute { get; set; }
        public CardStatus Status { get; set; }

        public MonsterCard()
        {
            CardCategory = (int)Base.CardCategory.Monster;
            Status = new CardStatus();
        }

        public MonsterCard(string password)
        {
            Models.Card card_m = DuelUtils.GetCard(password);
            Initialize(card_m);
        }

        public MonsterCard(Models.Card card_m)
        {
            Initialize(card_m);
        }

        private void Initialize(Models.Card card_m)
        {
            CardCategory = card_m.Category;
            CardText = card_m.Ctext;
            Name = card_m.Name;
            Cname = card_m.Cname;
            Password = card_m.Password;
            ATK = card_m.ATK;
            DEF = card_m.DEF;
            Attribute = card_m.Attribute;
            CardType = card_m.CardType;
            Level = card_m.Level;
            MonsterType = card_m.MonsterType;
            SummonedAttribute = card_m.SummonedAttribute;
            ChooseTargetType = card_m.ChooseTargetType;
            Status = new CardStatus();
            UID = Guid.NewGuid().ToString();
        }

        public virtual void Banished()
        {

        }

        public virtual void ComeIntoField()
        {

        }

        public virtual void ComeIntoGrave()
        {

        }

        public virtual void Destroyed()
        {

        }

        public virtual void Sacrificed()
        {

        }
    }

    [MoonSharpUserData]
    public class SpellAndTrapCard : Card
    {
        public int Icon { get; set; }
        public CardStatus Status { get; set; }
        public SpellAndTrapCard()
        {
            UID = Guid.NewGuid().ToString();
            Status = new CardStatus();
        }
        
        public SpellAndTrapCard(Models.Card card_m)
        {
            Icon = card_m.Icon;
            CardCategory = card_m.Category;
            CardText = card_m.Ctext;
            Name = card_m.Name;
            Cname = card_m.Cname;
            Password = card_m.Password;
            ChooseTargetType = card_m.ChooseTargetType;
            UID = Guid.NewGuid().ToString();
            Status = new CardStatus();
        }
    }
}
