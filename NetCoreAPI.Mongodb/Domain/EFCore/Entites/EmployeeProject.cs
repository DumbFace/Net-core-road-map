using Infrastucture.Domain.EFCore.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("EmployeeProject")]
    public class EmployeeProject : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }
    }
}