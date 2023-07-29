using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик запроса списка пользователей
    /// </summary>
    public sealed class GetUserHandler : BaseHandler<GetUserQuery, IEnumerable<GetUsersResponse>>
    {
        public GetUserHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService) { }

        public async override Task<IEnumerable<GetUsersResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var filter = _appDbContext.Users
                .Where(u => request.Name == null || u.Name.Contains(request.Name))
                .Where(u => request.MinRegistrationDateTime == default || u.CreatedOn >= request.MinRegistrationDateTime)
                .Where(u => request.MaxRegistrationDateTime == default || u.CreatedOn <= request.MaxRegistrationDateTime.AddDays(1));

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
