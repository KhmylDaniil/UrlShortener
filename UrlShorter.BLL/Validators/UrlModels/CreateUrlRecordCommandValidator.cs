using FluentValidation;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.BLL.Validators.UrlModels
{
    /// <summary>
    /// Валидатор для команды создания короткого url
    /// </summary>
    public sealed class CreateUrlRecordCommandValidator : AbstractValidator<CreateUrlRecordCommand>
    {
        public CreateUrlRecordCommandValidator()
        {
            RuleFor(x => x.LongUrl).NotEmpty().WithMessage(BLL.Constants.ExceptionMessages.FieldCantBeEmpty);
        }
    }
}
