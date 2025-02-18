using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("Comments")]
    public class Comment : BaseEntityStackOverflow
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int? Score { get; set; }

        [Required]
        [MaxLength(700)]
        public string Text { get; set; }

        public int? UserId { get; set; }

        //TODO

        public User User { get; set; }

        public Post Post { get; set; }
    }
}
