using AutoMapper;
using LegalCRM.Data;
using LegalCRM.Shared.Case;
using LegalCRM.Shared.Client;

namespace LegalCRM.Api.Mapping
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            // DTO -> Entity (зыпись)
            CreateMap<StayCaseCreateDto, StayCase>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore());

            // Entity -> DTO(чтение)
            CreateMap<Case, CaseReadDto>();
        }
    }
}
