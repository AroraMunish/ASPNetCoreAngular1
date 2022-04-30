using Microsoft.AspNetCore.Identity;

namespace GreatWorld.Data
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }

}
