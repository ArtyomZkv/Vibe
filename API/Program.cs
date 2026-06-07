using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EfCore;
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
builder.Services.AddScoped<ISmsService, Infrastructure.Services.FakeSmsService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.Interfaces.ISmsService).Assembly));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddScoped<IMatchRepository, MatchRepository>();

builder.Services.AddScoped<IDialogRepository, DialogRepository>();

builder.Services.AddScoped<IVerificationCodeStore, RedisVerificationCodeStore>();

builder.Services.AddExceptionHandler<API.ExceptionHandling.GlobalExceptionHandler>();

builder.Services.AddValidatorsFromAssemblyContaining<API.Validators.RegisterUserRequestValidator>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Connection"];
});

builder.Services.AddProblemDetails();


builder.Services.AddDbContext<Infrastructure.Persistence.AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

