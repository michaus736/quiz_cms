using QuizVistaApi.Middlewares;
using QuizVistaApiInfrastructureLayer.Extensions;


var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.ConfigureDatabaseConnection(configuration);
builder.Services.AddRepositories();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<AntiXssMiddleware>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AntiXssMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
