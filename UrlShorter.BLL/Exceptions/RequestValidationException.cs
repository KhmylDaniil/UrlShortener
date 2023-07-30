namespace UrlShortener.BLL.Exceptions
{
    /// <summary>
    /// Исключение при ошибке валидации модели в обработчике
    /// </summary>
    public sealed class RequestValidationException : ApplicationSystemBaseException
    {
        public RequestValidationException(string message) : base(message)
        {
        }
    }
}
