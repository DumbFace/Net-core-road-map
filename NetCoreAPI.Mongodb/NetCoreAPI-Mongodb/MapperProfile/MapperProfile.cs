using AutoMapper;
using NetCoreAPI_Mongodb.Entities;
using NetCoreAPI_Mongodb.Models;

namespace NetCoreAPI_Mongodb.MapperProfile
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
