using MediatR;

namespace UrlShorter.BLL.Models
{
    /// <summary>
    /// Команда удаления пользователя
    /// </summary>
    public class DeleteUserCommand : IRequest<Unit>
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
