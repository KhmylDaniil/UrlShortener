using FluentValidation;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.BLL.Validators.UrlModels
{
    /// <summary>
    /// Валидатор для команды удаления записей
    /// </summary>
    public sealed class DeleteUrlRecordCommandValidator : AbstractValidator<DeleteUrlRecordsCommand>
    {
        public DeleteUrlRecordCommandValidator()
        {
            RuleFor(x => x.Days).Must(x => x == null || x.Value >= 0).WithMessage(BLL.Constants.ExceptionMessages.FieldCantBeNegative);
        }
    }
}
