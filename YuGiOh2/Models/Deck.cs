using System.ComponentModel.DataAnnotations;

namespace YuGiOh2.Models
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverPassword { get; set; }
        public string Composition { get; set; }
        public string Text { get; set; }
    }
}
