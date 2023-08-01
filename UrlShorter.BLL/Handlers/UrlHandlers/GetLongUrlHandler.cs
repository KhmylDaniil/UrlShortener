using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.BLL.Handlers.UrlHandlers
{
    public sealed class GetLongUrlHandler : BaseHandler<GetLongUrlQuery, string>
    {
        public GetLongUrlHandler(IAppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async override Task<string> Handle(GetLongUrlQuery request, CancellationToken cancellationToken)
        {
            var existingRecord =  await _appDbContext.UrlRecords.FirstOrDefaultAsync(x => x.ShortUrl == request.ShortUrl, cancellationToken)
                ?? throw new EntityNotFoundException<UrlRecord>();

            return existingRecord.LongUrl;
        }
    }
}
