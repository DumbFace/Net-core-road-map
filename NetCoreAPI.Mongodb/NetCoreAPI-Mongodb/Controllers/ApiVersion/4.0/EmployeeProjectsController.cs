//using Domain.EFCore.Entites;
//using Infrastucture.EFCore;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace NetCoreAPI_Mongodb.Controllers.ApiVersion._3._0
//{
//    public class EmployeeProjectsController : ControllerBase
//    {
//        private readonly ExampleDbContext _context;

//        public EmployeeProjectsController(ExampleDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/EmployeeProjects
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<EmployeeProject>>> GetEmployeeProjects()
//        {
//            return await _context.EmployeeProjects.ToListAsync();
//        }

//        // GET: api/EmployeeProjects/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<EmployeeProject>> GetEmployeeProject(Guid id)
//        {
//            var employeeProject = await _context.EmployeeProjects.FindAsync(id);

//            if (employeeProject == null)
//            {
//                return NotFound();
//            }

//            return employeeProject;
//        }

//        // PUT: api/EmployeeProjects/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEmployeeProject(Guid id, EmployeeProject employeeProject)
//        {
//            if (id != employeeProject.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(employeeProject).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EmployeeProjectExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/EmployeeProjects
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<EmployeeProject>> PostEmployeeProject(EmployeeProject employeeProject)
//        {
//            _context.EmployeeProjects.Add(employeeProject);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetEmployeeProject", new { id = employeeProject.Id }, employeeProject);
//        }

//        // DELETE: api/EmployeeProjects/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEmployeeProject(Guid id)
//        {
//            var employeeProject = await _context.EmployeeProjects.FindAsync(id);
//            if (employeeProject == null)
//            {
//                return NotFound();
//            }

//            _context.EmployeeProjects.Remove(employeeProject);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EmployeeProjectExists(Guid id)
//        {
//            return _context.EmployeeProjects.Any(e => e.Id == id);
//        }
//    }
//}
