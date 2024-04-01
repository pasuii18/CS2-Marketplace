using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using FluentValidation;

namespace CSMarketApp.Infrastructure.Business.Algorithms;

public class UserValidator : AbstractValidator<UserCreateDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .MinimumLength(4).WithMessage("Login must be at least 4 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-zA-Z])(?=.*\d).+$").WithMessage("Password must contain at least one letter and one digit.");
    }
}