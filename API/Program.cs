using Infrastructure.Repositories;

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

builder.Services.AddSingleton<Application.Interfaces.IUserRepository, InMemoryUserRepository>();

builder.Services.AddSingleton<Application.Interfaces.ILikeRepository, InMemoryLikeRepository>();

builder.Services.AddSingleton<Application.Interfaces.IMatchRepository, InMemoryMatchRepository>();

builder.Services.AddSingleton<Application.Interfaces.IDialogRepository, InMemoryDialogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

