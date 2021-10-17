using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ClientWeb.Controllers
{
    public class ForgetPassController : Controller
    {
        // GET: ForgetPass
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CallRestMethod(string username, string email)
        {
            string url = string.Format("{0}?username={1}&email={2}", System.Configuration.ConfigurationManager.AppSettings["URLForgetPassAPI"], username, email);
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