using RestAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestAPI.DataContext;
using FluentMigrator.Runner;
using System.Reflection;
using MySql.Data.MySqlClient;
using RestAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<DapperDatabase>();

//Configure FluentMigrator on startup
builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
        .AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddMySql5()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("dappermigrationexample"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//Include Repos
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

//Solve CORS issue 
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//For JWT Token Configuration
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	var Key = Encoding.UTF8.GetBytes( builder.Configuration["JWT:Key"]);
	o.SaveToken = true;
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Key)
	};
});
//Register Repo to Test JWT Authentication
builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();

//Register for Automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration["ConnectionStrings:MySqlConnection"]));


var app = builder.Build();

//Auto migrate DB, Tables on application startup
/*using (var scope = app.Services.CreateScope())
{
    var databaseService = scope.ServiceProvider.GetRequiredService<DapperDatabase>();
    var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    try
    {
        databaseService.CreateDatabase("dappermigrationexample");
        migrationService.ListMigrations();
        migrationService.MigrateUp();
    }
    catch
    {
        //log errors or
        throw;
    }
}*/
app.MigrateDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

