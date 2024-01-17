using AutoMapper;
using Housing.API.Data.Interfaces;
using Housing.API.DTOS;
using Housing.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.API.Controllers
{

    [Authorize]
    public class CityController : BaseController
    {

        // private readonly DataContext _context;
        // private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            // _context = context;
            //  _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           
        }
        [HttpGet("city")]
      [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var result = await _unitOfWork.CityRepository.GetCitiesAsync();
            var mappedcities = _mapper.Map<IEnumerable<CityDto>>(result);
            //var citiesDto = from c in result
            //                select new CityDto()
            //                {
            //                    Id = c.Id,
            //                    Name = c.Name
            //                };
            return Ok(mappedcities);
        }

        //localhost/city/api/add?cityName=cairo
        [HttpPost("add")]
        public async Task<IActionResult> AddCity(CityDto citydto)
        {
            // if (ModelState.IsValid) return BadRequest();
            var mappedcity = _mapper.Map<City>(citydto);
            _unitOfWork.CityRepository.AddCity(mappedcity);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity(CityDto citydto)
        {
            var city = await _unitOfWork.CityRepository.FindCity(citydto.Id);
            if (city is null) return BadRequest();
            city.Name = citydto.Name;
            city.Country = citydto.Country;

            await _unitOfWork.SaveAsync();
            return StatusCode(201);
            //var mappedcity = _mapper.Map<City>(citydto);
            //var result = _unitOfWork.CityRepository.FindCity(mappedcity.Id);
            //_unitOfWork.CityRepository.AddCity(mappedcity);
            //await _unitOfWork.SaveAsync();
            //return StatusCode(201);

        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _unitOfWork.CityRepository.DeleteCity(id);

            await _unitOfWork.SaveAsync();
            return Ok(id);

        }
    }
}
