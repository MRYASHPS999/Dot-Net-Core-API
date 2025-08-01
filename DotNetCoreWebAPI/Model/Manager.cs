using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebAPI.Model
{
    public class Manager
    {

        [Key]
        public int Mid { get; set; }
        public string Mname { get; set; }

        public List<Employee> emps { get; set; }


    }

}
