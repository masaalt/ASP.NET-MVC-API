using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMT_API.App_Start;
using MongoDB.Bson;
using RMT_API.Models;
using MongoDB.Driver.Builders;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace RMT_API.Controllers
{
    public class AdminController : Controller
    {
        MongoContext _dbContext;
        Result _result;
        string Token = ConfigurationManager.AppSettings["Token"]; //Token  
        string TokenExpirationDays = ConfigurationManager.AppSettings["TokenExpirationDays"]; //TokenExpirationDays  
        public AdminController()
        {
            _dbContext = new MongoContext();
        }
        // GET: Admin
        [HttpGet]
        [Route("Register")]
        public JsonResult Register(Users users)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            _CreatePasswordHash(users.password, out passwordHash, out passwordSalt);
            users.passwordhash = passwordHash;
            users.passwordsalt = passwordSalt;
            var document = _dbContext._database.GetCollection<BsonDocument>("Users");
            var query = Query.EQ("phone", users.phone);
            var count = document.FindAs<Users>(query).Count();
            if (count == 0)
            {
                var result = document.Insert(users);
                _result.status = "success";
                _result.message = "با موفقیت ثبت نام شد";
            }
            else
            {
                _result.status = "failure";
                _result.message = "ناموفق";
            }
            return Json(_result);
        }
        //[HttpGet]
        //[Route("Login")]
        //public JsonResult Login(string username, string password)
        //{
        //    Users user = new Users();
        //    var document = _dbContext._database.GetCollection<BsonDocument>("Users");
        //    var query = Query.EQ("username", username);
        //    var count = document.FindAs<Users>(query).Count();
        //    if (count == 0)
        //    {
        //        user = document.FindAs<Users>(query).FirstOrDefault();
        //        if (_VerifyPasswordHash(password, user.passwordhash, user.passwordsalt))
        //        {
        //            var claims = new[] { new
        //        Claim(JwtRegisteredClaimNames.Jti,
        //        Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Name, user.username) };
        //            var tokenSecretKey = Encoding.UTF8.GetBytes(Token);
        //            var key = new SymmetricSecurityKey(tokenSecretKey);
        //            var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //            var tokenExpirationDays = Convert.ToInt32(TokenExpirationDays);
        //            var tokenDescriptor = new SecurityTokenDescriptor
        //            {
        //                Subject = new ClaimsIdentity(claims),
        //                Expires = DateTime.Now.AddDays(tokenExpirationDays),
        //                SigningCredentials = creadentials
        //            };
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var token = tokenHandler.CreateToken(tokenDescriptor);
        //            _result.status = "success";
        //            _result.message = "ورود با موفقیت";
        //            _result.token = token.ToString();
        //        }
        //        else
        //        {
        //            _result.status = "failure";
        //            _result.message = "ناموفق";
        //        }
        //    }
        //    else
        //    {
        //        _result.status = "failure";
        //        _result.message = "ناموفق";
        //    }
        //    return Json(_result);
        //}
        [HttpGet]
        [Route("Packages")]
        public JsonResult Packages()
        {
            try
            {
                //var packages = _dbContext._database.GetCollection<Packages>("Packages").FindAll().ToList();
                List<Packages> packageslist = new List<Packages>();

                for (int i = 0; i < 5; i++)
                    {
                    Packages packages = new Packages();
                        packages.date = ""+i.ToString();
                        packages.metadata = new ObjectId("586ed9c2578b8212afb3fc86");
                        packages.name = "" + i.ToString();
                        packages.package = "" + i.ToString();
                        packageslist.Add(packages);
                    }

                return Json(packageslist, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);

            }

        }
        [HttpGet]
        [Route("Packagess")]
        public JsonResult Packagess(string pid)
        {
            try
            {
                var packageid = Query<Packages>.EQ(p=>p.metadata, new ObjectId(pid));
                var packageDetails = _dbContext._database.GetCollection<PackageDetails>("PackageDetails").FindOne(packageid);
                return Json(packageDetails);
            }
            catch (Exception e)
            {
                return Json(e.ToString());

            }

        }
        [HttpGet]
        [Route("Package/Create")]
        public JsonResult PackageCreate(Packages package)
        {
            var document = _dbContext._database.GetCollection<BsonDocument>("Packages");
            var query = Query.EQ("name", package.name);
            var count = document.FindAs<Packages>(query).Count();
            if (count == 0)
            {
                var id = ObjectId.GenerateNewId();
                package.metadata = id;
                var result = document.Insert(package);
                _result.status = "success";
                _result.message = "با موفقیت ثبت نام شد";
                _result.metadata = id;
            }
            else
            {
                _result.status = "failure";
                _result.message = "ناموفق";
            }
            return Json(_result);
        }

        [HttpGet]
        [Route("Package/Notifications")]
        public JsonResult PackageNotifications(string pid)
        {
            ObjectId id = new ObjectId(pid);
            try
            {
                var packageid = Query<Packages>.EQ(p => p.metadata, new ObjectId(pid));
                var db = _dbContext._database;
                var PackageNotifsid = Query<PackageNotifs>.EQ(p => p.packageId, new ObjectId(pid));

                var packagenotifs = _dbContext._database.GetCollection<PackageNotifs>("PackageNotifs").FindOne(PackageNotifsid);
                return Json(packagenotifs);

            }
            catch (Exception e)
            {
                return Json(e.ToString());

            }

        }

 
 /// <summary>
 /// ///////////////////////////////////////////////
 /// </summary>
 /// <param name="password"></param>
 /// <param name="passwordHash"></param>
 /// <param name="passwordSalt"></param>
        //Calculate password hash and salt
        private void _CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool _VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
