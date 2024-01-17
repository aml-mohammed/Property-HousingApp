using AutoMapper;
using Housing.API.Data.Interfaces;
using Housing.API.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.API.Controllers
{

    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyTypeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //api/PropertyType/list
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertTypes()
        {
            var propertyTypes = await _unitOfWork.PropertTypeRepository.GetPropertyTypesAsync();
            var mappedPropertyTypes = _mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(mappedPropertyTypes);
        }

        //api/PropertyType/list
        [HttpGet("Flist")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingtTypes()
        {
            var FurnishingTypes = await _unitOfWork.PropertTypeRepository.GetFurnishingTypesAsync();
            var mappedFurnishingTypes = _mapper.Map<IEnumerable<KeyValuePairDto>>(FurnishingTypes);
            return Ok(mappedFurnishingTypes);
        }
    }
}
