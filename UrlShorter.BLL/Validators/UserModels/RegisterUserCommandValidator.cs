using FluentValidation;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.BLL.Validators.UserModels
{
    /// <summary>
    /// Валидатор команды регистрации пользователя
    /// </summary>
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(Constants.ExceptionMessages.FieldCantBeEmpty);

            RuleFor(x => x.Name).MaximumLength(12).WithMessage(Constants.ExceptionMessages.MaxFieldLength);

            RuleFor(x => x.Login).NotEmpty().WithMessage(Constants.ExceptionMessages.FieldCantBeEmpty);

            RuleFor(x => x.Login).MaximumLength(12).WithMessage(Constants.ExceptionMessages.MaxFieldLength);

            RuleFor(x => x.Password).NotEmpty().WithMessage(Constants.ExceptionMessages.FieldCantBeEmpty);

            RuleFor(x => x.Password).MinimumLength(5).WithMessage(Constants.ExceptionMessages.MaxFieldLength);

            RuleFor(x => x.Password).MaximumLength(20).WithMessage(Constants.ExceptionMessages.MaxFieldLength);
        }
    }
}
