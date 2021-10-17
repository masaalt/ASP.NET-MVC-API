using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class PackageDetail
    {
        [BsonElement("date")]
        public ObjectId date { get; set; }
        [BsonId]
        public string metaData { get; set; }
    }
}