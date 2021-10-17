using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RMT_API.Models
{
    public class PackageDetails
    {
        PackageDetail packagedetail { get; set; }
        List<PackageUsers> users { get; set; }
    }
}