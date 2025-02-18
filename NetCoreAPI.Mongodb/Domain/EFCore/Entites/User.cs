using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("Users")]
    public class User : BaseEntityStackOverflow
    {
        public string AboutMe { get; set; }

        public int? Age { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [MaxLength(40)]
        public string DisplayName { get; set; }

        [Required]
        public int DownVotes { get; set; }

        [MaxLength(40)]
        public string EmailHash { get; set; }

        [Required]
        public DateTime LastAccessDate { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        public int Reputation { get; set; }

        [Required]
        public int UpVotes { get; set; }

        [Required]
        public int Views { get; set; }

        [MaxLength(200)]
        public string WebsiteUrl { get; set; }

        public int? AccountId { get; set; }

        //TODO
        public ICollection<Comment> Comments { get; set; }

        public Badge Badge { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
