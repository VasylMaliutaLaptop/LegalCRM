using LegalCRM.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LegalCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _userService.RegisterUserAsync(model.UserName, model.Email, model.Password);
            if (result.Succeeded)
                return Ok("Пользователь успешно зарегистрирован");
            else
                return BadRequest(result.Errors);
        }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
