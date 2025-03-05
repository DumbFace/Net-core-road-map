using Common.Models.BaseModels;
using Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Mongodb.Controllers.BaseController;

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion._2._0
{
    public class EmployeesController : BaseController_v2
    {
        private readonly ExampleDbContext _context;

        public EmployeesController(ExampleDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
        {
            var query = _context.Employees.Select(employee => new EmployeeModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address = employee.Address,
                City = employee.City,
                CreatedTime = employee.CreatedTime,
                Email = employee.Email,
                Gender = employee.Gender,
                PhoneNumber = employee.PhoneNumber,
                Projects = employee.EmployeeProjects
                                    .Where(employeeProject => employeeProject.EmployeeId == employee.Id)
                                    .Select(employeeProjects => new ProjectModel()
                                    {
                                        Name = employeeProjects.Project.Name,
                                        Description = employeeProjects.Project.Description,
                                        EndDate = employeeProjects.Project.EndDate,
                                        StartDate = employeeProjects.Project.StartDate,
                                    })
            });


            var employees = await query.ToListAsync();
            return employees;
        }

        //// GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// PUT: api/Employees/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Employees
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        //// DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(Guid id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EmployeeExists(Guid id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
