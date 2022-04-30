using System.ComponentModel.DataAnnotations;

namespace GreatWorld.Models
{
    public class Stop
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [DataType(DataType.Date),
          DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Arrival { get; set; }
        public int Order { get; set; }
        public int Tripid { get; set; }
    }
}
