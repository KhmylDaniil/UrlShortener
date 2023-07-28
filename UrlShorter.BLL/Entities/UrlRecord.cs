using System.ComponentModel.DataAnnotations;

namespace UrlShorter.BLL.Entities
{
    public class UrlRecord : EntityBase
    {
        [StringLength(6)]
        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        public Guid? UserId { get; set; }

        public User? User { get; set; }
    }
}
