using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Features.Auth.SendVerificationCode
{
    public record SendVerificationCodeCommand(string PhoneNumber) : IRequest<Unit>;
}
