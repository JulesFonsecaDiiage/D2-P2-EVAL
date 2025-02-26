using BLL.Service;
using BLL.ServiceContract;
using DAL.Repository;
using DAL.RepositoryContract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add DbContext
builder.Services.AddDbContext<DAL.AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// Add Services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();


var app = builder.Build();

app.Use(async (context, next) =>
{
	if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
	{
		context.Response.StatusCode = 401;
		await context.Response.WriteAsync("API Key is missing.");
		return;
	}

	var apiKey = builder.Configuration["ApiKey"]; // Stocke la clé dans appsettings.json
	if (!apiKey.Equals(extractedApiKey))
	{
		context.Response.StatusCode = 403;
		await context.Response.WriteAsync("Unauthorized.");
		return;
	}

	await next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
