using MediatR;
using UrlShortener.BLL.Abstractions;

namespace UrlShortener.BLL.Handlers
{
    /// <summary>
    /// Базовый обработчик команд
    /// </summary>
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IAppDbContext _appDbContext;
        protected readonly IAuthorizationService _authorizationService;

        protected BaseHandler(IAppDbContext appDbContext, IAuthorizationService authorizationService)
        {
            _appDbContext = appDbContext;
            _authorizationService = authorizationService;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
