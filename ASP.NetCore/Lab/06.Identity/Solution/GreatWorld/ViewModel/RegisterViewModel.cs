using System.ComponentModel.DataAnnotations;

namespace GreatWorld.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ComparePassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
