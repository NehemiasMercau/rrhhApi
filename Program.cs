using Microsoft.EntityFrameworkCore;
using rrhhApi.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Utilizo Serilog para guardar información de logs en un archivo externo
builder.Host.UseSerilog();
// builder.Host.UseSerilog((ctx, lc) => lc
//     .WriteTo.Console(outputTemplate: "{Level:w4}: {SourceContext}[0] {NewLine}      {Message:lj}{Exception}{NewLine}"));

// builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
//                     loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();

builder.Services.AddDbContext<RRHHContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("RRHHContext"))
    .EnableSensitiveDataLogging(true));


builder.Services.AddEndpointsApiExplorer();

//Añade descripción al archivo de Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "RRHH API",
        Description = "ASP.NET Core Web API para administrar candidatos con sus empleos",
       
    });

    //Utilizo Reflection para crear un archivo XML, que contenga los comentarios de cada método para incluirlos en el archivo de Swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//Logs con Serilog, en un archivo que en este caso se encuentra en la raíz del proyecto, pero podría ir en la URL que se asigne al File()
Log.Logger = new LoggerConfiguration().WriteTo.File("./", rollingInterval: RollingInterval.Day).CreateLogger();
// Log.Logger = new LoggerConfiguration().CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
