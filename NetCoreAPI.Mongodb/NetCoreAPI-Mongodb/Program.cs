using Common.Common.MapperProfile;
using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;
using Infrastucture.Repository.EmployeeRepository;
using Infrastucture.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using NetCoreAPI_Mongodb.Data;
using NetCoreAPI_Mongodb.SignalRHub;
using NetCoreAPI_Mongodb.TempService;
using static NetCoreAPI_Mongodb.Data.MongoDBService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.ToString());
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
    c.SwaggerDoc("v3", new OpenApiInfo { Title = "My API V3", Version = "v3" });
    c.SwaggerDoc("v4", new OpenApiInfo { Title = "My API V4", Version = "v4" });
});
builder.Services.AddDbContext<ExampleDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ExampleDbContext")));

builder.Services.AddDbContext<StackOverflowDBContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("StackOverflowDBContext")));

builder.Services.AddDbContext<SecondDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("SecondDbContext")),
  ServiceLifetime.Scoped
  );

//builder.Services.AddSignalRCore();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IChatHubService, ChatHubService>();
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);


builder.Services.AddSignalR();

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:3000/")
//                .AllowAnyHeader()
//                .WithMethods("GET", "POST")
//                .AllowCredentials();
//        });
//});
builder.Services.AddScoped<IUnitOfWork<ExampleDbContext>, UnitOfWork<ExampleDbContext>>();
builder.Services.AddScoped<IUnitOfWork<SecondDbContext>, UnitOfWork<SecondDbContext>>();
builder.Services.AddScoped<IUnitOfWork<StackOverflowDBContext>, UnitOfWork<StackOverflowDBContext>>();
//builder.Services.AddScoped<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IRepository<Employee, ExampleDbContext>, Repository<Employee, ExampleDbContext>>();


builder.Services.Configure<MongoDBDatabaseSettings>(
    builder.Configuration.GetSection("MongoDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
        c.SwaggerEndpoint("/swagger/v3/swagger.json", "My API V3");
        c.SwaggerEndpoint("/swagger/v4/swagger.json", "My API V4");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseCors();
app.MapHub<ChatHub>("/chathub");
//app.MapHub<ChatHub>("/chathub/{group}");

app.UseRouting();

app.MapControllers();

app.Run();
