using Microsoft.EntityFrameworkCore;
using Student.API;
using Serilog;



var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/generallog.txt",
    //.WriteTo.File(builder.Configuration.GetValue<string>("_Logging:GeneralLogFilePath"),
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
    rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Starting Micro Service Student API");

// Add services to the container.

var connectionString = Environment.GetEnvironmentVariable("STUDENT_DB_CONNECTION_STRING");
builder.Services.AddDbContext<StudentContext>(
    options => options.UseNpgsql(
        connectionString
    )
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<StudentContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
