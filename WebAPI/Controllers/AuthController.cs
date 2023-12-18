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
            if (result.Success)
            {
                return Ok(new { Message = "Kaydınız başarılı bir şekilde oluşturuldu", Data = result.Data });
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto loginUserDto)
        {
            var loginResult = _authService.Login(loginUserDto);
            if (loginResult.Success)
            {
                var result = _authService.CreateAccessToken(loginResult.Data);
                if (result.Success)
                {
                    return Ok(new { Message = "Sisteme giriş başarılı", Data = result.Data });
                }
                return BadRequest(result);
            }
            return BadRequest(loginResult);
        }
    }
}
