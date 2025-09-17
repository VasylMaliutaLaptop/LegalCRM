using AutoMapper;
using LegalCRM.Api.Services;
using LegalCRM.Data;
using LegalCRM.Shared.Case;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegalCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController(AppDbContext context, IMapper mapper, ClientService clientService) : Controller
    {
        [HttpPost("addStayCase")]
        public async Task<IActionResult> AddStay(StayCaseCreateDto dto)
        {
            if (dto is null) return BadRequest("Case cannot be null");

            var exists = await context.Clients.AnyAsync(c => c.Id == dto.ClientId);
            if (!exists)
            {
                dto.ClientId = await clientService.EnsureClientAsync(dto.ClientId);
            }

            var entity = mapper.Map<StayCase>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            context.Cases.Add(entity);
            await context.SaveChangesAsync();

            return Ok(entity.Id);
        }
        [HttpGet("getCases")]
        public async Task<IActionResult> GetAll()
        {
            return NotFound();
        }
    }
}
