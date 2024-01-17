using Housing.API.Data.Interfaces;
using Housing.API.DTOS;
using Housing.API.Errors;
using Housing.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Housing.API.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto logindto)
        {
            var user = await _unitOfWork.UserRepository.AuthenticateUser(logindto.UserName, logindto.Password);
            if (user is null) return Unauthorized("invalid user id or passwod");

            var loginRespons = new LoginResponseDto();

            loginRespons.UserName = user.UserName;
            string mykey = _configuration["JWT:Key"];
            loginRespons.Token = CreateToken.CreateJWT(user,mykey);

            return Ok(loginRespons);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginDto logindto)
        {
            if (await _unitOfWork.UserRepository.UserAlreadyExist(logindto.UserName))
                return BadRequest(new ApiResponse(400,"User Already exists"));
            _unitOfWork.UserRepository.Register(logindto.UserName, logindto.Password);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);

        }


    }
}
