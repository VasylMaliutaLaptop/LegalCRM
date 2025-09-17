using AutoMapper;
using LegalCRM.Data;      // EF-сущности: Case, StayCase, Client
using LegalCRM.Shared.Case;   // DTO: CaseBase (+ CaseCreateDto/CaseReadDto, если разделяете)
using LegalCRM.Shared.Client; // DTO: ClientBase (+ ClientReadDto)

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
        }
    }
}
