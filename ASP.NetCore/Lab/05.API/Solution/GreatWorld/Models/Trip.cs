using System.ComponentModel.DataAnnotations;

namespace GreatWorld.Models
{
    public class Trip
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Trip Name")]
        public string? Name { get; set; }

        [DataType(DataType.Date),
          DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}",
                      ApplyFormatInEditMode = false)]
        [Display(Name = "Created On")]
        public DateTime Created { get; set; }

        public string? UserName { get; set; }

        [Display(Name = "Full Name")]
        [RegularExpression("^([a-zA-Z]+\\s[a-zA-Z]+)$",
              ErrorMessage =
            "Full Name must be two words with a space between first and last name")]
        public string? FullName { get; set; }

        public ICollection<Stop>? Stops { get; set; }
    }
}
