namespace UrlShorter.BLL.Exceptions
{
    /// <summary>
    /// Исключение при ошибке валидации модели в обработчике
    /// </summary>
    public class RequestValidationException : ApplicationSystemBaseException
    {
        public RequestValidationException(string message) : base(message)
        {
        }
    }
}
