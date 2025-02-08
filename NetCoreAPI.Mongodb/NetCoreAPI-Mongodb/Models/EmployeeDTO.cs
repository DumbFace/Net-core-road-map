using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetCoreAPI_Mongodb.Models
{
    public class EmployeeDTO
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? IpAddress { get; set; }
    }
}
