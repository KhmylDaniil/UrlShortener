namespace UrlShortener.BLL.Exceptions
{
    /// <summary>
    /// Базовая ошибка приложения
    /// </summary>
    public class ApplicationSystemBaseException : Exception
    {
        public ApplicationSystemBaseException(string message) : base(message)
        {
        }
    }
}
