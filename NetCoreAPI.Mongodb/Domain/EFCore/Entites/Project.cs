using Infrastucture.Domain.EFCore.Entites;
using Infrastucture.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFCore.Entites
{
    [Table("Project")]
    public class Project : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus Status { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
