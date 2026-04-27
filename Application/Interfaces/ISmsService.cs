using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISmsService
    {
        Task SendCodeAsync(string phoneNumber, string code);
    }
}
