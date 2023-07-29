using Microsoft.EntityFrameworkCore;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Models;

namespace UrlShorter.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик запроса списка пользователей
    /// </summary>
    public class GetUserHandler : BaseHandler<GetUserQuery, IEnumerable<GetUsersResponse>>
    {
        public GetUserHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService) { }

        public async override Task<IEnumerable<GetUsersResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var filter = _appDbContext.Users
                .Where(u => request.Name == null || u.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase))
                .Where(u => request.MinRegistrationDateTime == default || u.CreatedOn >= request.MinRegistrationDateTime)
                .Where(u => request.MaxRegistrationDateTime == default || u.CreatedOn <= request.MaxRegistrationDateTime);

            return await filter.Select(x => new GetUsersResponse
            {
                Id = x.Id,
                Name = x.Name,
                RegistrationDateTime = x.CreatedOn,
                Role = Enum.GetName(x.RoleType)
            }).ToListAsync(cancellationToken);
        }
    }
}
