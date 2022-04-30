using GreatWorld.Repository;
using GreatWorld.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using GreatWorld.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;

namespace GreatWorld.Controllers.API
{
    [Route("/api/trips")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TripController : ControllerBase
    {
        private IGreatWorldRepository _repository;
        private IMapper _mapper;
        private IMemoryCache _cache;

        public TripController(IGreatWorldRepository repository,
                                    IMapper mapper,
                                    IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpGet("/api/trips/{TripName?}")]
        //[HttpGet("/api/Trips.{format}"), FormatFilter]
        //[ResponseCache(CacheProfileName = "Default30")]
        //[ResponseCache(Duration = 120)]
        public IActionResult Get(string? TripName)
        {
            //user not logged-in
            if (User.Identity.Name == null)
            {
                if (TripName != null)
                {
                    var trip = _repository.GetTripByName(TripName);
                    return Ok(trip);
                }

                //Check for All Trips
                IEnumerable<TripViewModel> alltrips = null;
                if (_cache.TryGetValue("ListOfTrips", out alltrips) == false)
                {
                    if (alltrips == null)
                    {
                        alltrips = _mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWithStops());
                    }
                    var cacheOptions = new MemoryCacheEntryOptions()
                                          .SetSlidingExpiration(TimeSpan.FromSeconds(15));
                    _cache.Set("ListOfTrips", alltrips);
                }

                return Ok(alltrips);

            }
            else
            {
                if (TripName != null)
                {
                    var results = _repository.GetUserTripByName(User.Identity.Name, TripName); ;
                    return Ok(results);
                }
                IEnumerable<TripViewModel> alltrips = null;
                if (_cache.TryGetValue("ListOfTrips", out alltrips) == false)
                {
                    if (alltrips == null)
                    {
                        alltrips = _mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllUserTripsWithStops(User.Identity.Name));
                    }
                    var cacheOptions = new MemoryCacheEntryOptions()
                                          .SetSlidingExpiration(TimeSpan.FromSeconds(15));
                    _cache.Set("ListOfTrips", alltrips);
                }

                return Ok(alltrips);

                //var results1 = _mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllUserTripsWithStops(User.Identity.Name));
                //return Ok(results1);

            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] TripViewModel newTrip)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Trip nTrip = _mapper.Map<Trip>(newTrip);

                    //save to DB

                    //Add the trip
                    _repository.AddTrip(nTrip);

                    //If save succeeds, update the status code and send back the details
                    if (_repository.SaveAll() == true)
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Ok(_mapper.Map<TripViewModel>(nTrip));
                    }
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new { Message = false, ModelError = allErrors });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
