using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories.EfCore;
using Infrastructure.Repositories.InMemory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Регистрируем сервисы в DI-контейнере
builder.Services.AddScoped<Application.Interfaces.ISmsService, Infrastructure.Services.FakeSmsService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.Interfaces.ISmsService).Assembly));

builder.Services.AddScoped<Application.Interfaces.IUserRepository, UserRepository>();

builder.Services.AddScoped<Application.Interfaces.ILikeRepository, LikeRepository>();

builder.Services.AddScoped<Application.Interfaces.IMatchRepository, MatchRepository>();

builder.Services.AddScoped<Application.Interfaces.IDialogRepository, DialogRepository>();


builder.Services.AddValidatorsFromAssemblyContaining<API.Validators.RegisterUserRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDbContext<Infrastructure.Persistence.AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

