namespace UrlShorter.BLL.Exceptions
{
    /// <summary>
    /// Базовая ошибка отсутствия необходимого параметра
    /// </summary>
    /// <typeparam name="T">Тип, при работе которого появилось исключение</typeparam>
    public class ApplicationSystemNullException<T> : ApplicationSystemBaseException
    {
        public ApplicationSystemNullException(string argument)
            : base($"При работе класса {typeof(T)} отсутствует необходимый параметр {argument}.")
        {
        }
    }
}
