using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;
using Infrastucture.UnitOfWork;

namespace Infrastucture.Repository.EmployeeRepository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork<ExampleDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
