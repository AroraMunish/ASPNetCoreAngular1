using GreatWorld.Repository;
using GreatWorld.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using GreatWorld.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GreatWorld.Controllers.API
{
    [Route("/api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private IGreatWorldRepository _repository;
        private IMapper _mapper;

        public TripController(IGreatWorldRepository repository,
                                    IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("/api/Trips.{format}"), FormatFilter]
        [HttpGet("/api/Trips")]
        public IActionResult Get()
        {
            var results = _mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWithStops());
            return Ok(results);

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
