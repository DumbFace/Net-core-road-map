using AutoMapper;
using Common.Common.Models;
using Infrastucture.Domain.EFCore.Entites;

namespace Common.Common.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<EmployeeDTO_v2, Employee>();
        }
    }
}
