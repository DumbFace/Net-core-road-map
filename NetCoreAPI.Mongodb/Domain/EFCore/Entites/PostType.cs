using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("PostTypes")]
    public class PostType : BaseEntityStackOverflow
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

    }
}
