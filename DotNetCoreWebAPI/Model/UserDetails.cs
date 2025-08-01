using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebAPI.Model
{
    public class UserDetails
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
   
        public string Password { get; set; }

        public string role { get; set; }

        //navigation properties
        public List<Employee> employees { get; set; }

        public List<Manager> managers { get; set; }



    }

}
