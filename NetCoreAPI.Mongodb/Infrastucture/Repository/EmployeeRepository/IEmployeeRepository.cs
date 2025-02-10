using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.Repository.Base;

namespace Infrastucture.Repository.EmployeeRepository
{
    public interface ICountryRepository : IRepository<Employee>
    {
    }
}
