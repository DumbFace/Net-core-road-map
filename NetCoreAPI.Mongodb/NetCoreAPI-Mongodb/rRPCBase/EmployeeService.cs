using Grpc.Core;
using GrpcEmployee;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
namespace NetCoreAPI_Mongodb.rRPCBase
{
    using Common = Common.Models.BaseModels;

    using GoogleFromDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
    public class EmployeeService : GrpcServiceEmployee.GrpcServiceEmployeeBase
    {
        private readonly ExampleDbContext _context;
        public EmployeeService(ExampleDbContext context)
        {
            _context = context;
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

            var employees = await (from employee in _context.Employees.Take(10)
                                   join employeeProject in _context.EmployeeProjects on employee.Id equals employeeProject.EmployeeId
                                   join _project in _context.Projects on employeeProject.ProjectId equals _project.Id into bigGroup
                                   select new Common.EmployeeModel
                                   {
                                       Address = employee.Address,
                                       City = employee.City,
                                       CreatedTime = employee.CreatedTime,
                                       Email = employee.Email,
                                       FirstName = employee.FirstName,
                                       Gender = employee.Gender,
                                       LastName = employee.LastName,
                                       PhoneNumber = employee.PhoneNumber,
                                       Projects = bigGroup.Select(element => new Common.ProjectModel
                                       {
                                           Description = element.Description,
                                           StartDate = element.StartDate,
                                           EndDate = element.EndDate,
                                           Name = element.Name,
                                           Status = element.Status,
                                       })
                                   }).ToListAsync();

            GetEmployeesResponse getEmployeesResponse = new GetEmployeesResponse();

            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    GrpcEmployee.EmployeeModel employeeModel = new()
                    {
                        Address = employee.Address,
                        City = employee.City,
                        Email = employee.Email,
                        FirstName = employee.FirstName,
                        Gender = (Gender)(int)employee.Gender,
                        LastName = employee.LastName,
                        PhoneNumber = employee.PhoneNumber,
                        //Projects= employee.Projects.Select(project => new GrpcProject.ProjectModel
                        //{
                        //    Description= project.Description,
                        //    EndDate =project.EndDate,
                        //    StartDate = project.StartDate,
                        //    Name = project.Name,
                        //    Status = project.Status,
                        //})
                    };

                    if (employee.Projects.Any())
                    {
                        foreach (var project in employee.Projects)
                        {
                            //Timestamp endDate;
                            //if (project.EndDate.HasValue)
                            //{
                            //    endDate = GoogleFromDateTime.FromDateTime(project.EndDate.Value);
                            //}
                            GrpcProject.ProjectModel projectModel = new()
                            {
                                Description = project.Description,
                                EndDate = GoogleFromDateTime.FromDateTime(project.EndDate.Value),
                                StartDate = GoogleFromDateTime.FromDateTime(project.StartDate),
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
