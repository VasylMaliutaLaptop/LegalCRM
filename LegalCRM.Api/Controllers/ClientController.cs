using LegalCRM.Api.Services;
using LegalCRM.Shared.Client;
using Microsoft.AspNetCore.Mvc;

namespace LegalCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientService clientService) : Controller
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var items = await clientService.GetListAsync();
            return Ok(items);
        }
        [HttpPost("addClient")]
        public async Task<IActionResult> Add(ClientCreateDto _clientCreateDto, CancellationToken ct = default)
        {
            if (_clientCreateDto == null)
                return BadRequest("Client cannot be null");
            var id = await clientService.CreateAsync(_clientCreateDto, ct);

            return Ok(id);
        }
    }
}
