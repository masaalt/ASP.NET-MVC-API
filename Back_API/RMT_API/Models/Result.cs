using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace RMT_API.Models
{
    public class Result
    {
        public string status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public ObjectId metadata { get; set; }

    }
}