using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegalCRM.Data;
using LegalCRM.Shared.Case;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LegalCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController(AppDbContext context, IMapper mapper) : Controller
    {
        [HttpPost("addCase")]
        public async Task<IActionResult> Add(CaseCreateDto dto)
        {
            if (dto is null) return 
                    BadRequest("Case cannot be null");

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var entity = mapper.Map<Case>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UserId = userIdClaim;

            context.Cases.Add(entity);
            await context.SaveChangesAsync();

            return Ok(entity.Id);
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var items = await context.Cases
                .ProjectTo<CaseReadDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(items);
        }
    }
}
