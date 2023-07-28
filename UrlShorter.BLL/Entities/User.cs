using System.ComponentModel.DataAnnotations;

namespace UrlShorter.BLL.Entities
{
    public class User : EntityBase
    {
        protected User() { }

        public User(string name, string passwordHash)
        {
            Name = name;
            PasswordHash = passwordHash;
            UrlRecords = new();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<UrlRecord> UrlRecords { get; set; }
    }
}
