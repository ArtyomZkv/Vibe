using API.Contracts;
using FluentValidation;

namespace API.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[1-9]\d{10,14}$");

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(2,50);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
        }

    }
}
