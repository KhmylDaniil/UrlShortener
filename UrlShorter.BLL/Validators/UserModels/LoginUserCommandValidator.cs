using FluentValidation;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.BLL.Validators.UserModels
{
    /// <summary>
    /// Валидатор команды авторизации пользователя
    /// </summary>
    public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage(Constants.ExceptionMessages.FieldCantBeEmpty);

            RuleFor(x => x.Login).MaximumLength(12).WithMessage(Constants.ExceptionMessages.MaxFieldLength);

            RuleFor(x => x.Password).NotEmpty().WithMessage(Constants.ExceptionMessages.FieldCantBeEmpty);

            RuleFor(x => x.Password).MinimumLength(5).WithMessage(Constants.ExceptionMessages.MaxFieldLength);

            RuleFor(x => x.Password).MaximumLength(20).WithMessage(Constants.ExceptionMessages.MaxFieldLength);
        }
    }
}
