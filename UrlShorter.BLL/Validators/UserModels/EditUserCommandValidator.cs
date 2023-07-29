using FluentValidation;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.BLL.Validators.UserModels
{
    /// <summary>
    /// Валидатор для команды изменения пользователя
    /// </summary>
    public sealed class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(Constants.ExceptionMessages.FieldCantBeEmpty);

            RuleFor(x => x.Name).MaximumLength(12).WithMessage(Constants.ExceptionMessages.MaxFieldLength);

            RuleFor(x => x.SetNewPassword).Must(x => x is null || x.Length >= 5)
                .WithMessage(Constants.ExceptionMessages.PasswordIsTooShort);

            RuleFor(x => x.SetNewPassword).Must(x => x is null || x.Length <= 20)
                .WithMessage(Constants.ExceptionMessages.MaxFieldLength);
        }
    }
}
