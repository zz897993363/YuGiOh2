using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuGiOh2.Base
{
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
