using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("PostLinks")]
    public class PostLink : BaseEntityStackOverflow
    {
        //TODO Add relationship
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int RelatedPostId { get; set; }

        [Required]
        public int LinkTypeId { get; set; }
    }
}
