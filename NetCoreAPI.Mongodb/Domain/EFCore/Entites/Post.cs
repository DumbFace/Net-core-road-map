using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("Posts")]
    public class Post : BaseEntityStackOverflow
    {
        public int? AcceptedAnswerId { get; set; }

        public int? AnswerCount { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int? CommentCount { get; set; }

        public DateTime? CommunityOwnedDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public int? FavoriteCount { get; set; }

        [Required]
        public DateTime LastActivityDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        [MaxLength(40)]
        public string LastEditorDisplayName { get; set; }

        public int? LastEditorUserId { get; set; }

        public int? OwnerUserId { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public int PostTypeId { get; set; }

        [Required]
        public int Score { get; set; }

        [MaxLength(150)]
        public string Tags { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public int ViewCount { get; set; }
        //TODO

        public PostLink PostLink { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
