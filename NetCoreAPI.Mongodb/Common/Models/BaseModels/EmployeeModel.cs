using Infrastucture.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.BaseModels
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? CreatedTime { get; set; }

        [NotMapped]
        public IEnumerable<ProjectModel> Projects { get; set; }
    }
}
