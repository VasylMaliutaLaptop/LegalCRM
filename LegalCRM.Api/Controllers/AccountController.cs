using LegalCRM.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model, [FromServices] IConfiguration config)
        {
            var ok = await _userService.ValidateCredentialsAsync(model.UserName, model.Password);
            if (!ok) return Unauthorized("Неверные учетные данные");

            var jwt = config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iss, jwt["Issuer"]!)
        // добавьте роли/свои claims при необходимости
    };

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresMinutes"]!));
            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { access_token = accessToken, expires });
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me() => Ok(User.Claims.Select(c => new { c.Type, c.Value }));

    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
