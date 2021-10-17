using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class MessageObject
    {
        [BsonElement("style")]
        public int style { get; set; }
        [BsonElement("picture")]
        public string picture { get; set; }
        [BsonElement("title")]
        public string title { get; set; }
        [BsonElement("description")]
        public string description { get; set; }
    }
}