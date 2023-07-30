using MediatR;

namespace UrlShortener.BLL.Models.UserModels
{
    /// <summary>
    /// Запрос пользователя по идентификатору
    /// </summary>
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
    }
}
