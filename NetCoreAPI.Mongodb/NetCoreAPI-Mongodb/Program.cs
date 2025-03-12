using Common.Common.MapperProfile;
using GrpcGreeter;
using Infrastucture.AspnetCoreApi.Services.Interface;
using Infrastucture.AspnetCoreApi.Services.Services;
using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;
using Infrastucture.Repository.EmployeeRepository;
using Infrastucture.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCoreAPI_Mongodb.AuthorizeFilter;
using NetCoreAPI_Mongodb.Data;
using NetCoreAPI_Mongodb.rRPCBase;
using NetCoreAPI_Mongodb.SignalRHub;
using NetCoreAPI_Mongodb.TempService;
using Serilog;
using System.Text;
using Test.HandleException;
using static NetCoreAPI_Mongodb.Data.MongoDBService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectService(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
    options.SwaggerDoc("v3", new OpenApiInfo { Title = "My API V3", Version = "v3" });
    options.SwaggerDoc("v4", new OpenApiInfo { Title = "My API V4", Version = "v4" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Input your token here: Bearer {your_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
var Configuration = builder.Configuration;


// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenLocalhost(4000, options => { options.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2; options.UseHttps(); });
//     options.ListenLocalhost(5000, options => { options.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2; options.UseHttps(); });
// });
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration["Jwt:Issuer"],
        ValidAudience = Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
    };

    options.UseSecurityTokenValidators = true;
    options.SecurityTokenValidators.Clear();
    options.SecurityTokenValidators.Add(new CustomJwtSecurityTokenHandler());

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userPrincipal = context.Principal;
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.HandleResponse(); 
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"Unauthorized\"}");
        }
    };
});

builder.Services.AddSerilog();
Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs", rollingInterval: RollingInterval.Day)
            .CreateLogger();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ExampleDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("ExampleDbContext"));
    options.LogTo((queryString) =>
    {
        System.Diagnostics.Debug.WriteLine(queryString);
    });
});




builder.Services.AddDbContext<StackOverflowDBContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StackOverflowDBContext"));
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

Log.Logger = new LoggerConfiguration()
    //.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Tạo file log theo ngày
    //.MinimumLevel.Error()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddGrpc();
//builder.Services.AddGrpcClient<Greeter.Clien>
builder.Services.AddGrpcReflection();
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

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddSignalR();
//builder.Services.AddScoped<IConfigurationManager, ConfigurationManager>(); 
builder.Services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();
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
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("gRPC server is running...");
//    });
//    endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb()
//             .RequireCors("AllowAll");
//});
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.DefaultModelsExpandDepth(-1);
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//         c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
//         c.SwaggerEndpoint("/swagger/v3/swagger.json", "My API V3");
//         c.SwaggerEndpoint("/swagger/v4/swagger.json", "My API V4");
//     });

//     //app.MapGrpcReflectionService().AllowAnonymous();
// }
app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
        c.SwaggerEndpoint("/swagger/v3/swagger.json", "My API V3");
        c.SwaggerEndpoint("/swagger/v4/swagger.json", "My API V4");
    });
app.UseExceptionHandler();
//app.UseHttpsRedirection();

app.MapHub<ChatHub>("/chathub");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.MapGrpcService<GreeterService>().EnableGrpcWeb()
                                    .RequireCors("AllowAll");

app.MapGrpcService<EmployeeService>().EnableGrpcWeb();
app.MapGrpcService<UserService>().EnableGrpcWeb();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService().AllowAnonymous();
}

app.MapControllers();

app.Run();
