using AutoMapper;
using LegalCRM.Data;
using LegalCRM.Shared.Client;

namespace LegalCRM.Api.Mapping
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            // DTO -> Entity (зыпись)
            CreateMap<ClientCreateDto, Client>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Cases, o => o.Ignore());

            // Entity -> DTO(чтение)
            CreateMap<Client, ClientReadDto>()
                .ForMember(d => d.CaseIds, o => o.MapFrom(s => s.Cases.Select(c => c.Id)));
        }
    }
}
