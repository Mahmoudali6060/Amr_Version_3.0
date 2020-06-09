using FluentValidation;

namespace AMR_Server.Application.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.UserName)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
