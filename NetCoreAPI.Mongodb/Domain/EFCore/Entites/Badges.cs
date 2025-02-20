using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("Badges")]
    public class Badge : BaseEntityStackOverflow
    {
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }
        //TODO
        [NotMapped]
        public User? User { get; set; }
    }
}
