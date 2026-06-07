using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IVerificationCodeStore
    {
        Task SaveCode(string phoneNumber, string code, CancellationToken ct);
        Task<bool> VerifyCode(string phoneNumber, string codeFromUser, CancellationToken ct);
        Task DeleteCode(string phoneNumber, CancellationToken ct);
    }
}
