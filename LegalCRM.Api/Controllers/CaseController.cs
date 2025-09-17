using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var items = await context.Cases
                .ProjectTo<CaseReadDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(items);
        }
        //public async Task<IActionResult> GetAll()
        //{
        //    var cases = await context.Cases.ToListAsync();
        //    var dtos = mapper.Map<List<CaseReadDto>>(cases);
        //    return Ok(dtos);
        //}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await context.Cases
                .Where(c => c.Id == id)
                .ProjectTo<CaseReadDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (dto is null) return NotFound();
            return Ok(dto);
        }
    }
}
