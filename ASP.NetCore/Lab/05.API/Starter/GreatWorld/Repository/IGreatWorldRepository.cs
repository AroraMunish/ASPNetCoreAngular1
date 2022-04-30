using GreatWorld.Models;

namespace GreatWorld.Repository
{
    public interface IGreatWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
    }

}
