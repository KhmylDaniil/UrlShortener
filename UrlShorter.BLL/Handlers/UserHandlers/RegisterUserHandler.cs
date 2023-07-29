using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Constants;
using UrlShorter.BLL.Entities;
using UrlShorter.BLL.Exceptions;
using UrlShorter.BLL.Models;

namespace UrlShorter.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик команды регистрации пользователя
    /// </summary>
    public class RegisterUserHandler : BaseHandler<RegisterUserCommand, Guid>
    {
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService, IPasswordHasher passwordHasher)
            : base(appDbContext, authorizationService)
        {
            _passwordHasher = passwordHasher;
        }

        public override async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (_appDbContext.Users.Any(x => x.Login == request.Login))
                throw new RequestValidationException("Пользователь с таким логином уже зарегистрирован.");

            var user = new User(
                name: request.Name,
                login: request.Login,
                passwordHash: _passwordHasher.Hash(request.Password),
                roleType: request.AsAdmin ? RoleType.Admin : RoleType.User);

            await _appDbContext.Users.AddAsync(user, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
