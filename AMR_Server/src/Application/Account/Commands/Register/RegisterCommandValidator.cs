using FluentValidation;

namespace AMR_Server.Application.Account.Commands.Login
{
    public class RegisterCommandValidator : AbstractValidator<LoginCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(v => v.UserName)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
