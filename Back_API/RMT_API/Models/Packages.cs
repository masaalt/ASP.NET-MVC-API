using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class Packages
    {
        [BsonId]
        public ObjectId metadata { get; set; }
        [BsonElement("package")]
        public string package { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("date")]
        public string date { get; set; }
    }
}