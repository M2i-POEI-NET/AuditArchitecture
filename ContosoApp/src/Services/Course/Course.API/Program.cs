using Course.API;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/generallog.txt",
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
    rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Starting our service");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = Environment.GetEnvironmentVariable("COURSE_DB_CONNECTION_STRING");
builder.Services.AddDbContext<CourseContext>(
    options => options.UseNpgsql(
        connectionString
    )
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CourseContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
