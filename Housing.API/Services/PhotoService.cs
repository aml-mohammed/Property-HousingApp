using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Housing.API.Data.Interfaces;

namespace Housing.API.Services
{
    public class PhotoService : IPhotoService
    {
        // private readonly IConfiguration _configuration;

        private readonly Cloudinary cloudinary;
        public PhotoService(IConfiguration configuration)
        {
            Account account = new Account(
              configuration.GetSection("CloudinarySettings:CloudName").Value,
               configuration.GetSection("CloudinarySettings:APIKey").Value,
                configuration.GetSection("CloudinarySettings:APISecret").Value);

             cloudinary = new Cloudinary(account);
           
        }
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult= new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(800)
                };
                uploadResult =await cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await cloudinary.DestroyAsync(deleteParams);

            return result;

        }
    }
}
