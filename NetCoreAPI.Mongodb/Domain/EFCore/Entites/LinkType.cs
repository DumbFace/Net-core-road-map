using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("LinkTypes")]
    public class LinkType : BaseEntityStackOverflow
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
    }
}
