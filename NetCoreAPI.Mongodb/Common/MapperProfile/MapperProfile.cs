using AutoMapper;
using Common.Models.Bases;
using Common.Models.Mongo;
using Infrastucture.Domain.Mongo.Entities;
using EFCoreEntity = Infrastucture.Domain.EFCore.Entites;
namespace Common.Common.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Mongo Entity Employee 
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<EmployeeDTO_v2, Employee>();

            //EF Core Entity Employee
            CreateMap<EmployeeDTO, EFCoreEntity.Employee>().ReverseMap();
            //CreateMap<EFCoreEntity.Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO_v2, EFCoreEntity.Employee>();



            //EF Core Entity Employee

            CreateMap<EmployeeModel, EFCoreEntity.Employee>().ReverseMap();

        }
    }
}
