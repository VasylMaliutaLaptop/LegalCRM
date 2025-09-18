using AutoMapper;
using AutoMapper.QueryableExtensions;
using LegalCRM.Data;
using LegalCRM.Shared.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegalCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(AppDbContext context, IMapper mapper) : Controller
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var items = await context.Clients
                .AsNoTracking()
                .ProjectTo<ClientReadDto>(mapper.ConfigurationProvider)
                .ToListAsync(ct);
            return Ok(items);
        }
        [HttpPost("addClient")]
        public async Task<IActionResult> Add(ClientCreateDto clientCreateDto, CancellationToken ct = default)
        {
            if (clientCreateDto == null)
                return BadRequest("Client cannot be null");
            var entity = mapper.Map<Client>(clientCreateDto);
            context.Clients.Add(entity);
            await context.SaveChangesAsync(ct);

            return Ok(entity.Id);
        }
    }
}
