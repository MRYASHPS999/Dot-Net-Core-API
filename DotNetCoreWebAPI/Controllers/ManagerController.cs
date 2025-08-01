using DotNetCoreWebAPI.Data;
using DotNetCoreWebAPI.DTO;
using DotNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        ApplicationDbContext db;

        public ManagerController(ApplicationDbContext db) 
        {
            this.db = db;
        }

        [HttpPost]
        [Route("AddManager")] //this is attribute based routing where actionname is different and this route name which will appear on Swagger will be different
        public IActionResult AddNewManager(ManagerDTO m)
        {
            var man = new Manager()
            {
                Mname = m.Mname
            };

            db.manager.Add(man);
            db.SaveChanges();
            //return Ok("Employee Added Successfully");
            return Ok(new {message = $"{m.Mname} Added Successfully" } );

        }

        // below is example of using "FromForm" data annotations

        //[HttpPost]
        //[Route("AddManager")]
        //public IActionResult AddNewManager([FromForm] ManagerDTO m)
        //{
        //    var man = new Manager()
        //    {
        //        Mname = m.Mname
        //    };

        //    db.manager.Add(man);
        //    db.SaveChanges();
        //    return Ok("Employee Added Successfully");

        //}


        [HttpGet]
        [Route("FetchManager")]
        public IActionResult FetchManagerDetails() 
        { 
            var data = db.manager.ToList();
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteMgr/{eid}")]
        public IActionResult DeleteManagerById(int eid) 
        {
            var data = db.manager.Include(x => x.emps).FirstOrDefault(x => x.Mid == eid);

            if (data.emps.Count() != 0)
            {
                return Ok(new { message = "Employee Cannot Be Deleted" });
            }
            else 
            {
                db.manager.Remove(data);
                db.SaveChanges();
                return Ok(new { message = "Employee Deleted Successfully" });
            }

        }


    }


}
