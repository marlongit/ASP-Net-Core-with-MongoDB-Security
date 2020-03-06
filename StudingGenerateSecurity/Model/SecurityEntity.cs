using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StudingGenerateSecurity.Model
{
    public class SecurityEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("OId")]
        public Guid OId { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Account")]
        public string Account { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("DateCreate")]
        public DateTime DateCreate { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
    }
}
