using Business.Abstract;
using Entities.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto registerUserDto)
        {
            var registerResult = _authService.Register(registerUserDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto loginUserDto)
        {
            var userToLogin = _authService.Login(loginUserDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbymail")]
        public IActionResult GetByMail(string email)
        {
            var result = _authService.GetByMail(email);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
