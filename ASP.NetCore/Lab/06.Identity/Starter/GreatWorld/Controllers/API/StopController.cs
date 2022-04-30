using GreatWorld.Repository;
using GreatWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using GreatWorld.ViewModel;

using AutoMapper;
using System.Collections.Generic;

namespace GreatWorld.Controllers.API
{
    [Route("/api/trips/{tripName}/stops")]
    [ApiController]
    public class StopController : ControllerBase
    {

        private IGreatWorldRepository _repository;
        private IMapper _mapper;

        public StopController(IGreatWorldRepository repository,
                                    IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                Trip results = _repository.GetTripByName(tripName);
                if (results == null)
                {
                    return Ok(null);
                }
                return Ok(_mapper.Map<IEnumerable<StopViewModel>>(results.Stops));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
