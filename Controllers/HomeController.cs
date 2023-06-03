
using Microsoft.Ajax.Utilities; 
using MyFirstApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync("http://localhost:57933/Api/AllEmployee").Result;
            string readData=res.Content.ReadAsStringAsync().Result;
            var Data = JsonConvert.DeserializeObject<List<EmpModel>>(readData);
            return View(Data);


        }
        public ActionResult AddForm() {
        

            return View();
        }

        [HttpPost]
        public ActionResult AddForm(EmpModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            HttpClient httpClient= new HttpClient();    
            StringContent content = new StringContent(data,System.Text.Encoding.UTF8,"application/json");
            var res = httpClient.PostAsync("http://localhost:57933/Api/Add",content);
            return RedirectToAction("Index"); 
        }

        public ActionResult Delete(int Id)
        {

            HttpClient httpClient = new HttpClient();
            string url = "http://localhost:57933/Api/Delete?Id=" + Id + "";
            var res= httpClient.GetAsync(url).Result;
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int Id)
        {
           
          EmpModel model = new EmpModel();

            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync("http://localhost:57933/Api/Edit?Id="+Id+"").Result;
            string readData = res.Content.ReadAsStringAsync().Result;
            var models = JsonConvert.DeserializeObject<List<EmpModel>>(readData);
            
            model.Id = models.ToArray()[0].Id;
            model.Name = models.ToArray()[0].Name;
            model.City = models.ToArray()[0].City;

            return View(model);


            
            
            
        }


        [HttpPost]

        public ActionResult Edit(EmpModel model)
        {



            var data = JsonConvert.SerializeObject(model);
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            
            var res = httpClient.PostAsync("http://localhost:57933/Api/Update", content);

            return RedirectToAction("Index");

        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}