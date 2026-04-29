using Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Регистрируем сервисы в DI-контейнере
builder.Services.AddScoped<Application.Interfaces.ISmsService, Infrastructure.Services.FakeSmsService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.Interfaces.ISmsService).Assembly));

builder.Services.AddScoped<Application.Interfaces.IUserRepository, InMemoryUserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using(var scope = app.Services.CreateScope())
{
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    await mediator.Send(new Application.Features.Auth.SendVerificationCode.SendVerificationCodeCommand("123123123"));
}

using(var scope = app.Services.CreateScope())
{
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

    await mediator.Send(new Application.Features.Auth.RegisterUser.RegisterUserCommand(
        PhoneNumber: "89200634034",
        Name: "Артём",
        Gender: Domain.Enums.Gender.Male,
        RelationShip: Domain.Enums.RelationShip.Dating,
        DateOfBirth: new DateOnly(2001, 5, 14),
        Description: "Тестовое описание",
        City: "Москва"
        ));
}

app.Run();

