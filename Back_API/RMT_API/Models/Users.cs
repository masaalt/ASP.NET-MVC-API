using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class Users
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("phone")]
        public string phone { get; set; }
        [BsonElement("username")]
        public string username { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("passwordhash")]
        public byte[] passwordhash { get; set; }
        [BsonElement("passwordsalt")]
        public byte[] passwordsalt { get; set; }
    }
}