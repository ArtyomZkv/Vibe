using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Configurations;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Регистрируем сервисы в DI-контейнере
builder.Services.AddScoped<ISmsService, FakeSmsService>();
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

builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Key)),
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

