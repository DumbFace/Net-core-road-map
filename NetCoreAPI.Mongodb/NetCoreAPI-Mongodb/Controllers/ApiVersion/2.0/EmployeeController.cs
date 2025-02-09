using AutoMapper;
using Common.Common.Models;
using Infrastucture.Domain.Mongo.Entities;
using Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NetCoreAPI_Mongodb.Controllers.BaseController;
using NetCoreAPI_Mongodb.Data;
using static NetCoreAPI_Mongodb.Data.MongoDBService;

namespace NetCoreAPI_Mongodb.Controllers.Api.v2
{
    public class EmployeeController : BaseController_v2
    {
        readonly IMongoCollection<Employee>? employees;
        readonly IMongoCollection<EmployeeWithId>? employeesWithId;
        readonly IMapper mapper;
        readonly ExampleDbContext _dbContext;

        public EmployeeController(MongoDBService mongoDBService, IOptions<MongoDBDatabaseSettings> options, IMapper _mapper, ExampleDbContext dbContext)
        {
            employees = mongoDBService.Database?.GetCollection<Employee>(options.Value.EmployeesCollectionName);
            employeesWithId = mongoDBService.Database?.GetCollection<EmployeeWithId>("EmployeesWithId");
            if (employees == null)
            {
                throw new ArgumentNullException(nameof(employees));
            }
            _dbContext = dbContext;
            mapper = _mapper;
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
        public async Task<ActionResult> Create(EmployeeDTO_v2 employeeDTO)
        {
            var employee = mapper.Map<Employee>(employeeDTO);
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