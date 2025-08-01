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
    public class EmployeeController : ControllerBase
    {
        ApplicationDbContext db;

        public EmployeeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("AddEmployee")] //this is attribute based routing where actionname is different and this route name which will appear on Swagger will be different
        public IActionResult AddNewEmployee(EmployeeDTO emp)
        {
            var em = new Employee()
            {
                empname = emp.empname,
                email = emp.email,
                empsalary = emp.empsalary,
                Mid = emp.Mid

            };

            db.employee.Add(em);
            db.SaveChanges();
            //return Ok("Employee Added Successfully");
            return Ok(new { message = $"{em.empname} Added Successfully" });

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
        [Route("FetchEmployee")]
        public IActionResult FetchEmployeeDetails()
        {
            var data = db.employee.ToList();
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteEmp/{eid}")]
        public IActionResult DeleteEmployeeById(int eid)
        {
                var data = db.employee.Find(eid);

            if (data != null)
            {
                db.employee.Remove(data);
                db.SaveChanges();
                return Ok(new { message = "Employee Deleted Successfully" });
            }
            else 
            {
                return NotFound(new {message = "Employee Not Found" });
            }

            
                
            

        }

    }

}
