using System.ComponentModel.DataAnnotations;

namespace ZenturyBack.Models
{
    public class User
    {
        [Key,Required]
        public int Id { get; set; }

        [Required]
        public string  FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; } 

        public string Password  { get; set; }
    }
}
