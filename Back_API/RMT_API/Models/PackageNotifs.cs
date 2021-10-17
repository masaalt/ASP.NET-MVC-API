using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class PackageNotifs
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("packageId")]
        public ObjectId packageId { get; set; }
        [BsonElement("icon")]
        public string icon { get; set; }
        [BsonElement("date")]
        public string date { get; set; }
        [BsonElement("type")]
        public int type { get; set; }
        [BsonElement("messageObject")]
        public MessageObject messageObject { get; set; }
        [BsonElement("actionObject")]
        public ActionObject actionObject { get; set; }
        [BsonElement("customObject")]
        public object customObject { get; set; }
    }
}