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
    [Route("api/NationalPark")]
    [ApiController]
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository,IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nationalParkListDto = _nationalParkRepository.GetNationalParks().ToList().Select(_mapper.Map<NationalPark,NationalParkDto>);
            return Ok(nationalParkListDto);  //200
        }

        [HttpGet("{nationalparkId:int}",Name ="GetNationalPark")]
        public IActionResult GetNationalPark(int nationalparkId)
        {
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalparkId);
            if (nationalPark == null)
                return NotFound(); //404
            var nationalparkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalparkDto);//200

        }
        
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody]NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
                return BadRequest(ModelState); //400
            if(_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park already in DB");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalParkDto, NationalPark>(nationalParkDto);
            if(!_nationalParkRepository.CreateNationalPark(nationalPark)) //since bool type return true or false
            {
                ModelState.AddModelError("",$"Something went wrong while save data{ nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError,ModelState);
            }
          //  return Ok(); //200
          return CreatedAtRoute("GetNationalPark", new {nationalparkId=nationalPark.Id},nationalPark);//201 
        }

        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
                return BadRequest(ModelState);//400
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong while Update data{ nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);

            }
            // return Ok();
            return NoContent();//204
        }

        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if (nationalPark == null) return NotFound();
            if(!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong while Delete data{ nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();

        }
    }
}
