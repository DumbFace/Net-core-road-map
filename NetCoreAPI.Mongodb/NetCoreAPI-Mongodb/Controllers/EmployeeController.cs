using DnsClient.Protocol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NetCoreAPI_Mongodb.Data;
using NetCoreAPI_Mongodb.Entities;
using NetCoreAPI_Mongodb.Models;
using static NetCoreAPI_Mongodb.Data.MongoDBService;

namespace NetCoreAPI_Mongodb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        readonly IMongoCollection<Employee>? employees;
        readonly IMongoCollection<EmployeeWithId>? employeesWithId;

        public EmployeeController(MongoDBService mongoDBService, IOptions<MongoDBDatabaseSettings> options)
        {
            employees = mongoDBService.Database?.GetCollection<Employee>(options.Value.EmployeesCollectionName);
            employeesWithId = mongoDBService.Database?.GetCollection<EmployeeWithId>("EmployeesWithId");
            if (employees == null)
            {
                throw new ArgumentNullException(nameof(employees));
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await employees.Find(_ => true).ToListAsync();
        }

        [HttpGet]
        [Route("GetAPartFromEmployee")]
        public async Task<IEnumerable<EmployeeModel>> GetAPartFromEmployee()
        {
            var projection = Builders<Employee>.Projection.Expression(employee => new EmployeeModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName
            });

            return await employees
                .Find(_ => true)
                .Project(project => new EmployeeModel
                    {
                        FirstName = project.FirstName,
                        LastName = project.LastName
                    })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee?>> GetById(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var customer = await employees.Find(filter).FirstOrDefaultAsync();
            return customer is not null ? Ok(customer) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            await employees.InsertOneAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, employee.Id);
            var update = Builders<Employee>.Update
                .Set(employee => employee.FirstName, employee.FirstName)
                .Set(employee => employee.LastName, employee.LastName)
                .Set(employee => employee.Email, employee.Email)
                .Set(employee => employee.Gender, employee.Gender)
                .Set(employee => employee.IpAddress, employee.IpAddress);

            await employees.UpdateOneAsync(filter, update);

            return Ok(update);
        }
    }
}
