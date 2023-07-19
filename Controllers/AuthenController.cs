using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("Users")]
        public ActionResult GetAllUsers()
        {
            var GetAll = _authService.GetAllUsers();
            if (GetAll is null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(GetAll));
        }
        [HttpGet("Product/{id}")]
        public ActionResult GetUserById(Int32 id)
        {
            var GetName = _authService.GetUser(id);
            //return Ok(_productService.GetProduct(id));
            return new JsonResult(Ok(GetName));
        }
        [HttpPost("Login")]
        public ActionResult<UserLoginDtos> Login([FromBody] LoginDtos dto)
        {
            if (dto is null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
                return BadRequest();

            UserLoginDtos? userLoginDto = _authService.Login(dto);

            if (userLoginDto is null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(userLoginDto));
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDtos dto)
        {
            RegisterDtos? RegisterDto = _authService.Register(dto);

            if (RegisterDto is null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(RegisterDto));
        }
    }
}
