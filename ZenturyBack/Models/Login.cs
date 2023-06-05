using System.ComponentModel.DataAnnotations;

namespace ZenturyBack.Models
{
    public class Login
    {
        [Key, Required]
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; } 
    }
}
