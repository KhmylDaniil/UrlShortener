using MediatR;

namespace UrlShortener.BLL.Models.UrlModels
{
    /// <summary>
    /// Команда удаления записей из базы данных
    /// </summary>
    public sealed record DeleteUrlRecordsCommand : IRequest<Unit>
    {
        /// <summary>
        /// Фильтр по идентификатору пользователя
        /// </summary>
        public Guid? UserId { get; init; }

        /// <summary>
        /// Фильтр по давности создания в днях
        /// </summary>
        public int? Days { get; init; }

        public DeleteUrlRecordsCommand(Guid? userId, int? days)
        {
            UserId = userId;
            Days = days;
        }
    }
}
