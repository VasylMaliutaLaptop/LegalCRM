using AutoMapper;
using LegalCRM.Data;
using LegalCRM.Shared.Client;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LegalCRM.Api.Services
{
    public class ClientService(AppDbContext context, IMapper mapper)
    {

        public async Task<int> EnsureClientAsync(int? clientId, CancellationToken ct = default)
        {
            if (clientId is >= 0)
            {
                var exists = await context.Clients.AnyAsync(c => c.Id == clientId.Value, ct);
                if (exists) 
                    return clientId.Value;
                else
                {
                    var newClient = new ClientDTO
                    {
                        Status = ClientStatus.Draft,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "system"
                    };
                    await CreateAsync(newClient, ct);
                    return newClient.Id;
                }
            }
            throw new Exception($"ID is < 0");
        }

        public async Task<Client?> GetByIdAsync(int id, CancellationToken ct = default)
            => await context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<List<Client>> GetListAsync(CancellationToken ct = default)
            => await context.Clients.AsNoTracking().ToListAsync(ct);

        public async Task<ClientDTO> CreateAsync(ClientDTO dto, CancellationToken ct = default)
        {
            var entity = mapper.Map<Client>(dto);
            context.Clients.Add(entity);
            await context.SaveChangesAsync(ct);
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ClientDTO dto, CancellationToken ct = default)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (entity is null) return false;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = dto.UpdatedBy;

            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (entity is null) return false;
            context.Clients.Remove(entity);
            await context.SaveChangesAsync(ct);
            return true;
        }
    }
}
