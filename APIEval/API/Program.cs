using API.Middleware;
using BLL.Encryption;
using BLL.Service;
using BLL.ServiceContract;
using DAL.Repository;
using DAL.RepositoryContract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularDevClient",
		builder =>
		{
			builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed((host) => true);
		});
});

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
builder.Services.AddSingleton<IEncryptionStrategy, AesEncryptionStrategy>();

var app = builder.Build();

app.UseCors("AllowAngularDevClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
	app.UseHttpsRedirection();
}


app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
