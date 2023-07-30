using MediatR;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Команда удаления пользователя
    /// </summary>
    public sealed class DeleteUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
