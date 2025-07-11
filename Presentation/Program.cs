using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<BookingService>();


var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings-DefaultConnection2");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

//builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("BookingDatabaseConnection")));



builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
var app = builder.Build();



app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.UseCors("AllowAll");


app.Run();
