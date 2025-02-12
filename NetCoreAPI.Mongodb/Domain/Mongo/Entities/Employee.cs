using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Infrastucture.Domain.Mongo.Entities
{
    public class Employee
    {
        //[BsonId]
        //[BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        //public ObjectId _id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("first_name"), BsonRepresentation(BsonType.String)]
        public string FirstName { get; set; }

        [BsonElement("last_name"), BsonRepresentation(BsonType.String)]
        public string LastName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("gender"), BsonRepresentation(BsonType.String)]
        public string Gender { get; set; }

        [BsonElement("ip_address"), BsonRepresentation(BsonType.String)]
        public string IpAddress { get; set; }

        [BsonElement("age"), BsonRepresentation(BsonType.Int32)]
        public int? Age { get; set; }
    }
}
