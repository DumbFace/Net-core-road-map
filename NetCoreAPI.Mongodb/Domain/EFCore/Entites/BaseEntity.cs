using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastucture.Domain.EFCore.Entites
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? CreatedTime { get; set; } = DateTime.Now;

        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
    }
}
