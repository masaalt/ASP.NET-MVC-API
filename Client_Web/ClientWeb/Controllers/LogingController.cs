using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ClientWeb.Models;
namespace ClientWeb.Controllers
{
    public class LogingController : Controller
    {
        // GET: Login
        public ActionResult Index(string username, string password)
        {
            return View();
        }
        public ActionResult CallRestMethod(string username, string password)
        {
            string url = string.Format("{0}?username={1}&password={2}", System.Configuration.ConfigurationManager.AppSettings["URLLoginAPI"], username, password);
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = 0;
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                string result = string.Empty;
                result = responseStream.ReadToEnd();
                Result res = JsonConvert.DeserializeObject<Result>(result);
                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                if (res.status == "success")
                {
                    if (config.AppSettings.Settings["token_"+username]==null)
                    config.AppSettings.Settings.Add("token_"+ username, res.token);
                    else
                    config.AppSettings.Settings["token_" + username].Value = res.token;
                    config.Save();
                    return Json(Url.Action("Index", "Packages",new { username=username}));
                }

                webresponse.Close();
                ViewBag.Message = string.Format("Success {0}.\\nCurrent Date and Time: {1}", username, DateTime.Now.ToString());
                return null;
            }
            catch (Exception e)
            {
                ViewBag.Message = string.Format(e.ToString());
                return null;
            }


        }

    }
}