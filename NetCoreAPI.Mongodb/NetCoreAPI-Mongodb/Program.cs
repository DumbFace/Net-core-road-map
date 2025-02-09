using Common.Common.MapperProfile;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetCoreAPI_Mongodb.Data;
using static NetCoreAPI_Mongodb.Data.MongoDBService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
});
builder.Services.AddDbContext<ExampleDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ExampleDbContext")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

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

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<ExampleDbContext>();
//    context.Database.EnsureCreated();
//    DbInitializer.Initialize(context);
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();
