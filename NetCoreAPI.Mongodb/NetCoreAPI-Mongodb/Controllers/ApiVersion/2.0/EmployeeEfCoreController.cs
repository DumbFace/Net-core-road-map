using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Mongodb.Controllers.BaseController;

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion.v2
{
    public class EmployeeEfCoreController : BaseController_v2
    {
        readonly ExampleDbContext _context;
        public EmployeeEfCoreController(ExampleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var employee = _context.Employees.ToList();

            //using (var )
            return employee;
        }

    }
}
