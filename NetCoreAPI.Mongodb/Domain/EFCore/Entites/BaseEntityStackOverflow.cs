using System.ComponentModel.DataAnnotations;

namespace Domain.EFCore.Entites
{
    public class BaseEntityStackOverflow
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
