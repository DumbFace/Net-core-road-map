using Grpc.Core;
using GrpcEmployee;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

            public string ProjectAsString { get; set; }
        }

        public override async Task<GetEmployeesResponse> GetEmployees(Empty request, ServerCallContext context)
        {
            GetEmployeesResponse getEmployeesResponse = new();
 
            var employees = await _context.Employees.Select(employee => new EmployeeServiceResponseModel
            {
                Address = employee.Address,
                City = employee.City,
                CreatedTime = employee.CreatedTime,
                Email = employee.Email,
                FirstName = employee.FirstName,
                Gender = employee.Gender,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Projects = employee.EmployeeProjects.Select(employeeProject =>
                    new Common.ProjectModel
                    {
                        Description = employeeProject.Project.Description,
                        EndDate = employeeProject.Project.EndDate,
                        StartDate = employeeProject.Project.StartDate,
                        Name = employeeProject.Project.Name,
                        Status = employeeProject.Project.Status,
                    })
            })
            .Take(10)
            .ToListAsync();


            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    EmployeeModel employeeModel = new()
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

                    if (!String.IsNullOrEmpty(employee.ProjectAsString))
                    {
                        employee.Projects = JsonConvert.DeserializeObject<IEnumerable<Common.ProjectModel>>(employee.ProjectAsString);
                    }

                    if (employee.Projects is not null && employee.Projects.Any())
                    {
                        foreach (var project in employee.Projects)
                        {
                            project.StartDate = DateTime.SpecifyKind(project.StartDate, DateTimeKind.Utc);
                            project.EndDate = project.EndDate.HasValue ? DateTime.SpecifyKind(project.EndDate.Value, DateTimeKind.Utc) : null;

                            var timeStampStartDate = GoogleFromDateTime.FromDateTime(project.StartDate);
                            GoogleFromDateTime timeStampEndDate = null;
                            if (project.EndDate.HasValue)
                            {
                                timeStampEndDate = GoogleFromDateTime.FromDateTime(project.EndDate.Value);
                            }
                            GrpcProject.ProjectModel projectModel = new()
                            {
                                Description = project.Description,
                                StartDate = timeStampStartDate,
                                EndDate = timeStampEndDate,
                                Name = project.Name,
                                Status = (GrpcProject.ProjectStatus)(int)project.Status,
                            };
                            employeeModel.Projects.Add(projectModel);
                        }
                    }

                    getEmployeesResponse.Employees.Add(employeeModel);
                }
            }
            return await Task.FromResult(getEmployeesResponse);
        }
    }
}
