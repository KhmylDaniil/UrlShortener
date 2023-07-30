namespace UrlShortener.MVC.Models
{
    /// <summary>
    /// Модель прерывающей выполнение приложения ошибки
    /// </summary>
    public sealed class ErrorViewModel
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; }

        public ErrorViewModel(Exception ex)
        {
            Message = ex.Message;
        }
    }
}
