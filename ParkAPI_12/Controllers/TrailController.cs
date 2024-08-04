using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkAPI_12.Models;
using ParkAPI_12.Models.Dtos;
using ParkAPI_12.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_12.Controllers
{
    [Route("api/Trail")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trailRepository, IMapper mapper)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepository.GetTrails().ToList().Select(_mapper.Map<Trail, TrailDto>));
        }

        [HttpGet("{trailId:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepository.GetTrail(trailId);
            if (trail == null) return NotFound();
            return Ok(_mapper.Map<TrailDto>(trail));
        }

        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailDto trailDto)
        {
            if (trailDto == null) return BadRequest(ModelState);
            if (_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", $"Trail already in DB:{trailDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var trail = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while Save trail:{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateTrail([FromBody] TrailDto trailDto)
        {
            if (trailDto == null) return NotFound();
            var trail = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while Save trail:{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }


        [HttpDelete("{trailId:int}")]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId))
                return NotFound();
            var trail = _trailRepository.GetTrail(trailId);
            if(!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while delete trail:{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
        
        
    }
}
 