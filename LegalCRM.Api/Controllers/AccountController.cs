using LegalCRM.Data;
using LegalCRM.Shared.User;
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

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var user = new User
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email
            };

            var result = await userManager.CreateAsync(user, createUserDto.Password);

            if (result.Succeeded)
                return Ok("Пользователь успешно зарегистрирован");
            else
                return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login, [FromServices] IConfiguration config)
        {
            var user = await userManager.FindByNameAsync(login.UserName);
            if (user is null) 
                return NotFound("Пользователь не найден");

            var ok = await userManager.CheckPasswordAsync(user, login.Password);
            if (!ok) 
                return Unauthorized("Неверные учетные данные");

            var jwt = config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.Id),
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
            return Ok(new { AccessToken = accessToken, expires});
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me() => Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }
}