using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("VoteTypes")]
    public class VoteType : BaseEntityStackOverflow
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
