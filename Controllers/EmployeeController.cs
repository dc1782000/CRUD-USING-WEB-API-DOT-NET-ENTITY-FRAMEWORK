using MyFirstWebApi.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace MyFirstWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        
        [Route("Api/AllEmployee")]

        public List<Employee> Get()
        {
            DcEntities entities = new DcEntities();
            var res =entities.Employees.ToList();

            return res;
        }

        [HttpGet]
        [Route("Api/Delete")]
        public bool DeleteEmp(int id)
        {
            bool res = false;
            DcEntities entity = new DcEntities();
            var deleteData=entity.Employees.Where(a=>a.Id == id).FirstOrDefault();
            if(deleteData != null)
            {
                res = true;
                entity.Employees.Remove(deleteData);
                entity.SaveChanges();
            }
            return res;
        }
        [HttpPost]
        [Route("Api/Add")]
        public void AddEmp(Employee emp)
        {
            DcEntities dcEntities = new DcEntities();
            if(emp.Id == 0)
            {
                dcEntities.Employees.Add(emp);
                dcEntities.SaveChanges();
            }
        }
        [HttpGet]
        [Route("Api/Edit")]

        public List<Employee> Edit(int Id)
        {
            DcEntities entities = new DcEntities();
            List<Employee> data = new List<Employee>();
            var op = entities.Employees.Where(a => a.Id == Id).FirstOrDefault();
            data.Add(op);
            return data;
        }
        [HttpPost]
        [Route("API/Update")]
        public void UpdateEmp(Employee emp)
        {
            DcEntities db = new DcEntities();
            if (emp.Id != 0)
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}