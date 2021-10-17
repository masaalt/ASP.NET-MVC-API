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

namespace ClientWeb.Controllers
{
    public class AdminController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            string url = string.Format("{0}/", System.Configuration.ConfigurationManager.AppSettings["URLREST"], "");
            //var det= await GetResponseText(url);
            string details = CallRestMethod(url);
            return View();
        }
        public static string CallRestMethod(string url)
        {
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Method = "GET";
                webrequest.ContentType = "none";
               // webrequest.Headers.Add("Username", "xyz");
               // webrequest.Headers.Add("Password", "abc");
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                string result = string.Empty;
                result = responseStream.ReadToEnd();
                webresponse.Close();
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }

        
        }
        public static async Task<string> GetResponseText(string address)
        {
            return await httpClient.GetStringAsync(address);
        }
    }
}