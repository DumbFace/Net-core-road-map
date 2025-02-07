using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetCoreAPI_Mongodb.Models
{
    public class EmployeeModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
