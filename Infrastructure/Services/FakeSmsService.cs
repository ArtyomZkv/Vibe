using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class FakeSmsService : ISmsService
    {
        public Task SendCodeAsync(string phoneNumber, string code)
        {
            Console.WriteLine($"[FakeSms] Отправлен код {code} на номер {phoneNumber}");
            return Task.CompletedTask;
        }
    }
}
