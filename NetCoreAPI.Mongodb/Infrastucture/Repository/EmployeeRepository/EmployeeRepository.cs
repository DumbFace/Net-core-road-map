using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;

namespace Infrastucture.Repository.EmployeeRepository
{
    public class EmployeeRepository : Repository<Employee, ExampleDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(ExampleDbContext context) : base(context)
        {
            System.Diagnostics.Debug.WriteLine($"Employee repository context ID:  {context.ContextId}");
        }
    }
}
