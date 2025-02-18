using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("Votes")]
    public class Vote : BaseEntityStackOverflow
    {
        [Required]
        public int PostId { get; set; }

        public int? UserId { get; set; }

        public int? BountyAmount { get; set; }

        [Required]
        public int VoteTypeId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        //TODO
        public VoteType VoteType { get; set; }
    }
}
