using LegalCRM.Api.Services;
using LegalCRM.Shared.Client;
using Microsoft.AspNetCore.Mvc;

namespace LegalCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientService clientService) : Controller
    {
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var items = await clientService.GetListAsync();
            return Ok(items);
        }
        [HttpPost("addClient")]
        public async Task<IActionResult> Add(ClientDTO clientDTO, CancellationToken ct = default)
        {
            if (clientDTO == null)
                return BadRequest("Client cannot be null");
            await clientService.CreateAsync(clientDTO, ct);

            return Ok(clientDTO);
        }
    }
}
