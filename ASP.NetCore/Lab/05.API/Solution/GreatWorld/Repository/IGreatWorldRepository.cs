using GreatWorld.Models;

namespace GreatWorld.Repository
{
    public interface IGreatWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();

        void AddTrip(Trip nTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName);

    }

}
