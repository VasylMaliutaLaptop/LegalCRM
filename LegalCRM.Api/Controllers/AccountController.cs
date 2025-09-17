using LegalCRM.Data;
using LegalCRM.Shared.Contracts;
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
    public class AccountController(UserManager<User> userManager) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            var user = new User
            {
                UserName = register.UserName,
                Email = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
                return Ok("Пользователь успешно зарегистрирован");
            else
                return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login, [FromServices] IConfiguration config)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user is null) 
                return NotFound("Пользователь не найден");

            var ok = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!ok) 
                return Unauthorized("Неверные учетные данные");

            var jwt = config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, login.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Iss, jwt["Issuer"]!)
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
            return Ok(new { AccessToken = accessToken, expires });
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me() => Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }
}