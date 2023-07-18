using Microsoft.AspNetCore.Mvc;
using SampleAPI.Dtos;
using SampleAPI.Service;

namespace SampleAPI.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IAuthenService _authService;
        private object hello;

        public AuthenController(IAuthenService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public ActionResult<UserLoginDtos> Login([FromBody] LoginDtos dto)
        {
            if (dto is null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
                return BadRequest();

            UserLoginDtos? userLoginDto = _authService.Login(dto);

            if (userLoginDto is null)
            {
                return NotFound(Json("Failed"));
            }

            return Ok(Json("Success", userLoginDto));
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDtos dto)
        {
            RegisterDtos? RegisterDto = _authService.Register(dto);

            if (RegisterDto is null)
            {
                return BadRequest();
            }

            return Ok(RegisterDto);
        }
    }
}
