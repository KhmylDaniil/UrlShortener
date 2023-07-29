using UrlShorter.BLL.Entities;

namespace UrlShorter.BLL.Exceptions
{
    /// <summary>
    /// Исключение при невозможности найти сущность в базе данных
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class EntityNotFoundException<T> : ApplicationSystemBaseException where T : EntityBase
    {
        public EntityNotFoundException(Guid id)
            : base($"Не найдена сущность {typeof(T)} с ИД {id}.")
        {
        }
    }
}
