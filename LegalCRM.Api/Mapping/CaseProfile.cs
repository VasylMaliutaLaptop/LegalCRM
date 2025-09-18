using AutoMapper;
using LegalCRM.Data;
using LegalCRM.Shared.Case;

namespace LegalCRM.Api.Mapping
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            // DTO -> Entity (зыпись)
            CreateMap<CaseCreateDto, Case>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore());

            // Entity -> DTO(чтение)
            CreateMap<Case, CaseReadDto>();
        }
    }
}
