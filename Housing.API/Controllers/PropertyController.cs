using AutoMapper;
using Housing.API.Data.Interfaces;
using Housing.API.DTOS;
using Housing.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.API.Controllers
{

    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PropertyController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        //property/list/1
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperties(int sellRent)
        {
            var properties = await _unitOfWork.PropertyRepository.GetPropertiesAsync(sellRent);
            var mapedProperty = _mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(mapedProperty);
        }


        //property/detail/1
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetails(int id)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertyDetailsAsync(id);
            var mapedProperty = _mapper.Map<PropertyDetailDto>(property);
            return Ok(mapedProperty);
        }

        //property/add/1
        [HttpPost("add")]
       [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertydto)
        {
            var mapedProperty = _mapper.Map<Property>(propertydto);
            var userId = GetUserId();
            mapedProperty.LastUpdatedBy = userId;
            mapedProperty.PostedBy = userId;
            _unitOfWork.PropertyRepository.AddPropertyAsync(mapedProperty);
            await _unitOfWork.SaveAsync();

            return StatusCode(201);
        }
        [HttpPost("addd")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProperty2(PropertyDto propertydto)
        {
            var mapedProperty = _mapper.Map<Property>(propertydto);
           // var userId = GetUserId();
            mapedProperty.LastUpdatedBy = 1;
            mapedProperty.PostedBy = 1;
            _unitOfWork.PropertyRepository.AddPropertyAsync(mapedProperty);
            await _unitOfWork.SaveAsync();

            return StatusCode(201);
        }

        //property/add/photo/2
        [HttpPost("add/photo/{propId}")]
        [Authorize]
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file, int propId)
        {
            var result =await _photoService.UploadImageAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);
            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if(property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }
            property.Photos.Add(photo);
            await _unitOfWork.SaveAsync();
            
            return StatusCode(201);
        }

        //property/set-primary-photo/42/jl0sdfl20sdf2s
        [HttpPost("set-primary-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> SetPrimaryPhoto(int propId, string photoPublicId)
        {
            var userId = GetUserId();

            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised to change the photo");

            if (property == null || property.PostedBy != userId)
                return BadRequest("No such property or photo exists");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("No such property or photo exists");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo");


            var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
            if (currentPrimary != null) currentPrimary.IsPrimary = false;
            photo.IsPrimary = true;

            if (await _unitOfWork.SaveAsync()) return NoContent();

            return BadRequest("Failed to set primary photo");
        }


        //[HttpDelete("delete-photo/{propId}/{photoPublicId}")]
        //[Authorize]
        //public async Task<IActionResult> DeletePhoto(int propId, string photoPublicId)
        //{
        //    var userId = GetUserId();

        //    var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

        //    if (property.PostedBy != userId)
        //        return BadRequest("You are not authorised to delete the photo");

        //    if (property == null || property.PostedBy != userId)
        //        return BadRequest("No such property or photo exists");

        //    var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

        //    if (photo == null)
        //        return BadRequest("No such property or photo exists");

        //    if (photo.IsPrimary)
        //        return BadRequest("You can not delete primary photo");

        //    if (photo.PublicId != null)
        //    {
        //        var result = await _photoService.DeletePhotoAsync(photo.PublicId);
        //        if (result.Error != null) return BadRequest(result.Error.Message);
        //    }

        //    property.Photos.Remove(photo);

        //    if (await _unitOfWork.SaveAsync()) return Ok();

        //    return BadRequest("Failed to delete photo");
        //}


    }
}
