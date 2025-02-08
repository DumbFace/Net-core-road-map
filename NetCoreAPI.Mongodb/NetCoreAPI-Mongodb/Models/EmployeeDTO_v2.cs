using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace NetCoreAPI_Mongodb.Models
{
    public class EmployeeDTO_v2 : EmployeeDTO
    {
        [Required]
        public int? Age { get; set; }
    }
}
