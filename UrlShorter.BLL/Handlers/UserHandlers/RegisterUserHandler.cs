using MediatR;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Constants;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Models.UserModels;

namespace UrlShortener.BLL.Handlers.UserHandlers
{
    /// <summary>
    /// Обработчик команды регистрации пользователя
    /// </summary>
    public sealed class RegisterUserHandler : BaseHandler<RegisterUserCommand, Unit>
    {
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IAppDbContext appDbContext, IPasswordHasher passwordHasher) : base(appDbContext)
        {
            _passwordHasher = passwordHasher;
        }

        public override async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
            return Unit.Value;
        }
    }
}
