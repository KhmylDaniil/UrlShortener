using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Entities;
using UrlShorter.BLL.Exceptions;
using UrlShorter.BLL.Models;

namespace UrlShorter.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик команды удаления пользователя
    /// </summary>
    public class DeleteUserHandler : BaseHandler<DeleteUserCommand, Unit>
    {
        public DeleteUserHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService) : base(appDbContext, authorizationService) { }

        public async override Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException<User>(request.Id);

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
