using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;

namespace Infrastucture.Repository.EmployeeRepository
{
    public interface IEmployeeRepository : IRepository<Employee, ExampleDbContext>
    {
    }
}
