using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RMT_API.Models
{
    public class ActionObject
    {
        [BsonElement("action")]
        public int action { get; set; }
        [BsonElement("text")]
        public string text { get; set; }
    }
}