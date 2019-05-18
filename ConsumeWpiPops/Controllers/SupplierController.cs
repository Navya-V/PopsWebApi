using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ConsumeWpiPops.Controllers
{
    public class SupplierController : Controller
    {
        string Baseurl = "http://localhost:16727/Api/SUPPLIERs";
        // GET: T1
        public async Task<ActionResult> Index()
        {
            List<SUPPLIER> SupInfo = new List<SUPPLIER>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(Baseurl);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var SupResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    SupInfo = JsonConvert.DeserializeObject<List<SUPPLIER>>(SupResponse);

                }
                //returning the employee list to view  
                return View(SupInfo);
            }
        }

        public ActionResult Create()
        {
            return View(new SUPPLIER());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(SUPPLIER e)
        {
            using (var client = new HttpClient())
            {

                var response = client.PostAsync(Baseurl, new StringContent(
                    new JavaScriptSerializer().Serialize(e), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error");
            }
        }
    }
}
