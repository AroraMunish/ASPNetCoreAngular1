using System.ComponentModel.DataAnnotations;

namespace GreatWorld.ViewModel
{
    public class TripViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public IEnumerable<StopViewModel>? Stops { get; set; }


    }
}
