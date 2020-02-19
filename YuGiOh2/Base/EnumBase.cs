namespace YuGiOh2.Base
{
    public enum Attribute
    {
        Dark,
        Light,
        Earth,
        Water,
        Fire,
        Wind,
        Divine,
        Spell,
        Trap
    }

    public enum Icon
    {
        Normal,
        Equip,
        Field,
        QuickPlay,
        Ritual,
        Continuous,
        Counter
    }

    public enum MonsterType
    {
        Spellcaster,
        Dragon,
        Zombie,
        Warrior,
        BeastWarrior,
        Beast,
        WingedBeast,
        Fiend,
        Fairy,
        Insect,
        Dinosaur,
        Reptile,
        Fish,
        SeaSerpent,
        Aqua,
        Pyro,
        Thunder,
        Rock,
        Plant,
        Machine,
        Psychic,
        DivineBeast,
        Wyrm,
        Cyberse,
        None
    }

    public enum CardType
    {
        Normal = 1,
        Effect = 2,
        Ritual = 4,
        Fusion = 8,
        Synchro = 16,
        Xyz = 32,
        Toon = 64,
        Spirit = 128,
        Union = 256,
        Gemini = 512,
        Tuner = 1024,
        Flip = 2048,
        Pendulum = 4096,
        Link = 8192
    }

    public enum CardCategory
    {
        Monster,
        Spell,
        Trap
    }

    public enum SummonedAttribute
    {
        None,
        Dark,
        White,
        Devil,
        Fantasy,
        Fire,
        Forest,
        Wind,
        Earth,
        Thunder,
        Water,
        Divine
    }

    public enum ChooseTargetType
    {
        /// <summary>
        /// 无需选取对象
        /// </summary>
        None,
        
        SelfFaceUpMonster,
        SelfFaceDownMonster,
        SelfMonster,
        SelfFaceUpSpellAndTrap,
        SelfFaceDownSpellAndTrap,
        SelfSpellAndTrap,
        
        FoeFaceUpMonster,
        FoeFaceDownMonster,
        FoeMonster,
        FoeFaceUpSpellAndTrap,
        FoeFaceDownSpellAndTrap,
        FoeSpellAndTrap,

        AllFaceUpMonster,
        AllFaceDownMonster,
        AllMonster,
        AllFaceUpSpellAndTrap,
        AllFaceDownSpellAndTrap,
        AllSpellAndTrap,

        SelfMonsterInGrave,
        SelfSpellInGrave,
        SelfTrapInGrave,
        
        FoeMonsterInGrave,
        FoeSpellInGrave,
        FoeTrapInGrave,
        
        AllMonsterInGrave,
        AllSpellInGrave,
        AllTrapInGrave,
    }

    public enum AffectMomentType
    {
        /// <summary>
        /// 无法主动发动
        /// </summary>
        None,
        WhenSummoned,
        WhenAttacked
    }
}
