namespace UrlShorter.BLL.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedByUserId { get; set; }
    }
}
