using System.ComponentModel.DataAnnotations;

namespace YuGiOh2.Models
{
    public class Fusion
    {
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        [Key]
        public string PasswordResult { get; set; }
    }
}