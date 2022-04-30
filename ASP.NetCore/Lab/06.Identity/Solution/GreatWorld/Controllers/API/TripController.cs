using GreatWorld.Repository;
using GreatWorld.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using GreatWorld.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/api/trips/{TripName?}")]
        //[HttpGet("/api/Trips.{format}"), FormatFilter]
        public IActionResult Get(string? TripName)
        {
            if (TripName != null)
            {
                //var results = _repository.GetTripByName(TripName);
                Trip results = _repository.GetUserTripByName(User.Identity.Name, TripName); ;
                return Ok(results);
            }
            //var results1 = _mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWithStops());
            IEnumerable<TripViewModel> results1 = _mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllUserTripsWithStops(User.Identity.Name));
            return Ok(results1);
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
