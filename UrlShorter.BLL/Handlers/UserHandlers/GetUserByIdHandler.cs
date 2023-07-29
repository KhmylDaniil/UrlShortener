using Microsoft.EntityFrameworkCore;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Entities;
using UrlShorter.BLL.Exceptions;
using UrlShorter.BLL.Models;

namespace UrlShorter.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик запроса получения пользователя по идентификатору
    /// </summary>
    public class GetUserByIdHandler : BaseHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        public GetUserByIdHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService) { }

        public async override Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException<User>(request.Id);

            return new GetUserByIdResponse
            {
                Id = request.Id,
                Name = user.Name,
                RegistrationDateTime = user.CreatedOn,
                Role = Enum.GetName(user.RoleType),
                Login = user.Login,
                UrlRecordsCount = user.UrlRecordsCount
            };
        }
    }
}
