using Microsoft.EntityFrameworkCore;
using Student.API;

var builder = WebApplication.CreateBuilder(args);

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
