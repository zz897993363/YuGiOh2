using MoonSharp.Interpreter;

namespace YuGiOh2.Base
{
    [MoonSharpUserData]
    public class Field
    {
        public readonly MonsterCard[] MonsterFields;
        public readonly SpellAndTrapCard[] SpellAndTrapFields;
        public SpellAndTrapCard FieldField;
        public Field()
        {
            MonsterFields = new MonsterCard[5];
            SpellAndTrapFields = new SpellAndTrapCard[5];
        }
    }
}
