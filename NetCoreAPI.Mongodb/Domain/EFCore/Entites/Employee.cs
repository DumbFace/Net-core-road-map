using Domain.EFCore.Entites;
using Infrastucture.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infrastucture.Domain.EFCore.Entites
{
    [Table("Employee")]
    public class Employee : BaseEntity
    {
        [MaxLength(50)]
        [AllowNull]
        public string FirstName { get; set; }

        [AllowNull]
        [MaxLength(50)]
        public string LastName { get; set; }

        //Male = 1
        //Female = 2
        public Gender Gender { get; set; }

        [MaxLength(100)]
        [AllowNull]
        public string Address { get; set; }

        [MaxLength(100)]
        [AllowNull]
        public string City { get; set; }

        [MaxLength(20)]
        [AllowNull]
        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [AllowNull]
        [EmailAddress]
        public string email { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }

    }
}
