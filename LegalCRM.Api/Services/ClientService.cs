using AutoMapper;
using LegalCRM.Data;
using LegalCRM.Shared.Client;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LegalCRM.Api.Services
{
    public class ClientService(AppDbContext context, IMapper mapper)
    {
        public async Task<Client?> GetByIdAsync(int id, CancellationToken ct = default)
            => await context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<List<Client>> GetListAsync(CancellationToken ct = default)
            => await context.Clients.AsNoTracking().ToListAsync(ct);

        public async Task<int> CreateAsync(ClientCreateDto dto, CancellationToken ct = default)
        {
            var entity = mapper.Map<Client>(dto);
            context.Clients.Add(entity);
            await context.SaveChangesAsync(ct);
            return entity.Id;
        }
        public async Task<bool> UpdateAsync(int id, ClientReadDto clientReadDto, CancellationToken ct = default)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (entity is null) return false;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = clientReadDto.UpdatedBy;

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
