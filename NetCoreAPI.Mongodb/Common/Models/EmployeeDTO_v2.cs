using System.ComponentModel.DataAnnotations;

namespace Common.Common.Models
{
    public class EmployeeDTO_v2 : EmployeeDTO
    {
        [Required]
        public int? Age { get; set; }
    }
}
