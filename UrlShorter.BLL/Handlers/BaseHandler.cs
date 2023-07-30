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

        protected BaseHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
