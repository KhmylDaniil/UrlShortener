namespace UrlShorter.BLL.Abstractions
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }
    }
}
