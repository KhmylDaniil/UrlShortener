namespace UrlShorter.BLL.Entities
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Идентификатор создавшего пользователя
        /// </summary>
        public Guid CreatedByUserId { get; set; }
    }
}
