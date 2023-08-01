using System.Text;
using Base62;
using Microsoft.EntityFrameworkCore;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Entities;
using UrlShortener.BLL.Models.UrlModels;

namespace UrlShortener.BLL.Handlers.UrlHandlers
{
    public class CreateUrlRecordHandler : BaseHandler<CreateUrlRecordCommand, string>
    {
        private readonly IUserContext _userContext;
        
        public CreateUrlRecordHandler(IAppDbContext appDbContext, IUserContext userContext) 
            : base(appDbContext)
        {
            _userContext = userContext;
        }

        public async override Task<string> Handle(CreateUrlRecordCommand request, CancellationToken cancellationToken)
        {
            string shortUrl = ToShortUrl(request.LongUrl);

            UrlRecord existingRecord = await _appDbContext.UrlRecords
                .Include(x => x.Users).FirstOrDefaultAsync(x => x.ShortUrl == shortUrl, cancellationToken);

            User? currentUser = _userContext.RoleType == Constants.RoleType.Guest
                ? null
                : await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken);

            if (existingRecord is not null)
            {
                if (currentUser is not null)
                {
                    currentUser.UrlRecordsCount++;
                    existingRecord.Users.Add(currentUser);
                }
            }
            else
            {
                UrlRecord newRecord = new(shortUrl, request.LongUrl, currentUser);

                _appDbContext.UrlRecords.Add(newRecord);
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return shortUrl;
        }

        static string ToShortUrl(string url)
        {
            byte[] urlAsBytes = Encoding.ASCII.GetBytes(url);
            string urlAsHex = Convert.ToHexString(urlAsBytes);

            var base62Converter = new Base62Converter();
            var encoded = base62Converter.Encode(urlAsHex);

            return encoded[..7];
        }
    }
}
