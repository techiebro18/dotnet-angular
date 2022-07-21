using RestAPI.DataContext;
using RestAPI.Services;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

/*builder.Services.AddDbContext<RestAPI.DataContext.AppContext>(options =>
          options.UseMySql(
              Configuration.GetConnectionString("DefaultConnection")));*/

//Include Repos
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IDapper, Dapperr>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
