using System.ComponentModel.DataAnnotations;

namespace Common.Models.Mongo
{
    public class EmployeeDTO_v2 : EmployeeDTO
    {
        [Required]
        public int? Age { get; set; }
    }
}
