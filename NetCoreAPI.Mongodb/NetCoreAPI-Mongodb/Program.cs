using Microsoft.OpenApi.Models;
using NetCoreAPI_Mongodb.Data;
using NetCoreAPI_Mongodb.MapperProfile;
using static NetCoreAPI_Mongodb.Data.MongoDBService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddApiVersioning(option =>
//{

//    option.DefaultApiVersion = new ApiVersion(1, 0); // Default API version
//    option.AssumeDefaultVersionWhenUnspecified = true; // Allow access without specifying the version
//    option.ReportApiVersions = true; // Display supported versions
//    option.ApiVersionReader = new UrlSegmentApiVersionReader(); // Read version from URL (e.g., api/v1/employee)

//});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MongoDBDatabaseSettings>(
    builder.Configuration.GetSection("MongoDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
    });
}

app.UseHttpsRedirection();

//app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();
