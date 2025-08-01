using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreWebAPI.Model
{
    public class Employee
    {
        [Key]
        public int empid { get; set; }
        public string empname { get; set; }
        public string email { get; set; }
        public double empsalary { get; set; }

        [ForeignKey("Manager")]
        public int Mid { get; set; }
        public Manager Manager { get; set; }

    }

}
