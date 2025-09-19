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
                .ForMember(d => d.Cases, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore());

            // Entity -> DTO(чтение)
            CreateMap<Client, ClientReadDto>();

            CreateMap<ContactInfoDto, ContactInfo>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.ClientId, o => o.Ignore())
                .ForMember(d => d.Client, o => o.Ignore());

            // Entity -> DTO (вложенный)
            CreateMap<ContactInfo, ContactInfoDto>();

        }
    }
}
