using UrlShortener.BLL.Entities;

namespace UrlShortener.BLL.Exceptions
{
    /// <summary>
    /// Исключение при невозможности найти сущность в базе данных
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public sealed class EntityNotFoundException<T> : ApplicationSystemBaseException where T : EntityBase
    {
        public EntityNotFoundException(Guid id)
            : base($"Не найдена сущность {typeof(T).Name} с ИД {id}.")
        {
        }

        public EntityNotFoundException()
            : base($"Не найдена сущность {typeof(T).Name}.")
        {
        }
    }
}
