using AutoMapper;
using Common.Common.Models;
using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;
using Infrastucture.Repository.EmployeeRepository;
using Infrastucture.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    public class ExampleCRUDController : BaseController_v2
    {
        public IUnitOfWork<ExampleDbContext> _unitOfWork;
        public IEmployeeRepository _employeeRepository;
        public IRepository<Employee, ExampleDbContext> _repositoryEmployee;
        private readonly IMapper _mapper;

        public ExampleCRUDController(
            IUnitOfWork<ExampleDbContext> unitOfWork,
            IEmployeeRepository employeeRepository,
            IRepository<Employee, ExampleDbContext> repository,
            IMapper mapper

            )
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _repositoryEmployee = repository;
            _mapper = mapper;
            //_repositoryEmployee
            //System.Diagnostics.Debug.WriteLine($"Context ID Controller 1: {_unitOfWork.Context.ContextId}");
        }

        // GET: api/ExampleCRUD
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        public IEnumerable<Employee> GetEmployees()
        {
            IEnumerable<EmployeeDTO> collection = new List<EmployeeDTO>();

            //collection.Ad
            //var employees = _unitOfWork.Employees.GetAll();
            var employees = _repositoryEmployee.Context.Employees.ToList();
            var test = _repositoryEmployee
                        .Context
                        .Employees
                        //.Select(_mapper.ProjectTo<EmployeeDTO>)
                        .Select(employee => _mapper.Map<EmployeeDTO>(employee))
                        .ToList();
            //_unitOfWork.Employees.
            //var employees = await _context.Employees.ToListAsync();
            //var employees = await _unitOfWork.Context.Employees.ToListAsync();

            return employees;
            //return [];

        }

        // GET: api/ExampleCRUD/5
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

        //// PUT: api/ExampleCRUD/5
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

        //// POST: api/ExampleCRUD
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        //// DELETE: api/ExampleCRUD/5
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
