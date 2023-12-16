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
            var result = _authService.CreateAccessTokenForRegister(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto loginUserDto)
        {
            var result = _authService.Login(loginUserDto);
            if (result.Success)
            {
                var accessToken = _authService.CreateAccessTokenForLogin(result.Data);
                if (accessToken.Success)
                {
                    return Ok(accessToken);
                }
                return BadRequest(accessToken);
            }
            return BadRequest(result);
        }
    }
}
