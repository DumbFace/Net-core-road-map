using Grpc.Core;
using GrpcEmployee;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace NetCoreAPI_Mongodb.rRPCBase
{
    using Common = Common.Models.BaseModels;

    using GoogleFromDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
    public class EmployeeService : GrpcServiceEmployee.GrpcServiceEmployeeBase
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly ExampleDbContext _context;
        public EmployeeService(ExampleDbContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public class EmployeeServiceResponseModel : Common.EmployeeModel
        {
            public Guid Id { get; set; }
        }

        public override async Task<GetEmployeesResponse> GetEmployees(GrpcEmployee.Empty request, ServerCallContext context)
        {
            //var employees = await _context.Employees.Select(employee => new Common.EmployeeModel
            //{
            //    Address = employee.Address,
            //    City = employee.City,
            //    Email = employee.Email,
            //    FirstName = employee.FirstName,
            //    Gender = employee.Gender,
            //    LastName = employee.LastName,
            //    PhoneNumber = employee.PhoneNumber,
            //    Projects = employee.EmployeeProjects.Select(employeeProject => new Common.ProjectModel
            //    {
            //        Description = employeeProject.Project.Description,
            //        EndDate = employeeProject.Project.EndDate,
            //        StartDate = employeeProject.Project.StartDate,
            //        Name = employeeProject.Project.Name,
            //        Status = employeeProject.Project.Status,
            //    })
            //})
            //    .Take(10)
            //    .ToListAsync();

            GetEmployeesResponse getEmployeesResponse = new();

            var employees = await (from employee in _context.Employees.Take(10) 
                                   join employeeProject in _context.EmployeeProjects on employee.Id equals employeeProject.EmployeeId
                                   join project in _context.Projects on employeeProject.ProjectId equals project.Id into bigGroup
                                   where employee.Id == new Guid("D159082A-728D-4937-A56F-052693C19B11")
                                   select new EmployeeServiceResponseModel
                                   {
                                       Id = employee.Id,
                                       Address = employee.Address,
                                       City = employee.City,
                                       CreatedTime = employee.CreatedTime,
                                       Email = employee.Email,
                                       FirstName = employee.FirstName,
                                       Gender = employee.Gender,
                                       LastName = employee.LastName,
                                       PhoneNumber = employee.PhoneNumber,
                                       Projects = bigGroup

                                   }).ToListAsync();

            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    //if (employee.City.Contains("Jenkinsport"))
                    //{
                    //    _logger.LogInformation($"---DATETIME {employee.Projects.Select(project => project.StartDate).FirstOrDefault().ToString()}");
                    //}
                    GrpcEmployee.EmployeeModel employeeModel = new()
                    {
                        Id = employee.Id.ToString(),
                        Address = employee.Address,
                        City = employee.City,
                        Email = employee.Email,
                        FirstName = employee.FirstName,
                        Gender = (Gender)(int)employee.Gender,
                        LastName = employee.LastName,
                        PhoneNumber = employee.PhoneNumber,
                     
                    };

                    if (employee.Projects.Any())
                    {
                        foreach (var project in employee.Projects)
                        {
                            GrpcProject.ProjectModel projectModel = new()
                            {
                                Description = project.Description,
                                StartDate = GoogleFromDateTime.FromDateTime(project.StartDateCurrentTimeZone.ToUniversalTime()),
                                EndDate = project.EndDate.HasValue ? GoogleFromDateTime.FromDateTime(project.EndDateCurrentTimeZone.Value.ToUniversalTime()) : null,
                                Name = project.Name,
                                Status = (GrpcProject.ProjectStatus)(int)project.Status,
                            };
                            employeeModel.Projects.Add(projectModel);
                        }
                    }
                    getEmployeesResponse.Employees.Add(employeeModel);
                }
            }

            //getEmployeesResponse.Employees.AddRange(employees);
            return await Task.FromResult(getEmployeesResponse);
        }
    }
}
