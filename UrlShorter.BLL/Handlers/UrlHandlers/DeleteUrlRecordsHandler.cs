using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.BLL.Handlers.UrlHandlers
{
    /// <summary>
    /// Обработчик команды удаления записей по фильтрам
    /// </summary>
    public class DeleteUrlRecordsHandler : BaseHandler<DeleteUrlRecordsCommand, Unit>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public DeleteUrlRecordsHandler(
            IAppDbContext appDbContext,
            IAuthorizationService authorizationService,
            IDateTimeProvider dateTimeProvider)
            : base(appDbContext)
        {
            _dateTimeProvider = dateTimeProvider;
            _authorizationService = authorizationService;
        }

        public override async Task<Unit> Handle(DeleteUrlRecordsCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizationCheck(Constants.RoleType.Admin);

            DateTime? dateTimeFilter = request.Days is null
                ? null
                : _dateTimeProvider.Now.AddDays((double)request.Days);

            var recordsToDeleteFilter = _appDbContext.UrlRecords.Include(ur => ur.Users)
                .Where(r => dateTimeFilter == null || r.CreatedOn <= dateTimeFilter.Value)
                .Where(r => request.UserId == null
                || r.Users.Any() && r.Users.All(u => u.Id == request.UserId.Value));

            var aaa  = await recordsToDeleteFilter.ToListAsync(cancellationToken);

            _appDbContext.UrlRecords.RemoveRange(recordsToDeleteFilter);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
