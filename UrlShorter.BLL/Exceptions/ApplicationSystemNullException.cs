namespace UrlShortener.BLL.Exceptions
{
    /// <summary>
    /// Базовая ошибка отсутствия необходимого параметра
    /// </summary>
    /// <typeparam name="T">Тип, при работе которого появилось исключение</typeparam>
    public sealed class ApplicationSystemNullException<T> : ApplicationSystemBaseException
    {
        public ApplicationSystemNullException(string argument)
            : base($"При работе класса {typeof(T).Name} отсутствует необходимый параметр {argument}.")
        {
        }
    }
}
