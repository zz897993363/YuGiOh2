using System.ComponentModel.DataAnnotations;

namespace YuGiOh2.Models
{
    public class Card
    {
        [Key]
        public string Password { get; set; }
        public int Category { get; set; }
        public string Text { get; set; }
        public string Ctext { get; set; }
        public string Name { get; set; }
        public string Cname { get; set; }
        public int Icon { get; set; }
        public int Attribute { get; set; }
        public int Level { get; set; }
        public int MonsterType { get; set; }
        public int CardType { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int SummonedAttribute { get; set; }
        public int ChooseTargetType { get; set; }
    }
}
