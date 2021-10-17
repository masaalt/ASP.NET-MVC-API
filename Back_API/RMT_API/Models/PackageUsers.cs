using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class PackageUsers
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("serial")]
        public string serial { get; set; }
        [BsonElement("manufacture")]
        public string manufacture { get; set; }
        [BsonElement("deviceModel")]
        public string deviceModel { get; set; }
        [BsonElement("height")]
        public int height { get; set; }
        [BsonElement("width")]
        public int width { get; set; }
        [BsonElement("ip")]
        public string ip { get; set; }
    }
}