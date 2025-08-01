using DotNetCoreWebAPI.Data;
using DotNetCoreWebAPI.DTO;
using DotNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAuthController : ControllerBase
    {
        IConfiguration config;
        ApplicationDbContext db;

        public APIAuthController(IConfiguration config,ApplicationDbContext db) 
        {
            this.config = config;
            this.db = db;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserDTO log)
        {
            var user = db.users.FirstOrDefault(u => u.UserName == log.UserName && u.Password == log.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(UserDetails user)
        {
            var jwtSettings = config.GetSection("Jwt");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.role)
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("AdminData")]
        public IActionResult GetAdminData()
        {
            return Ok("Only Admins can access this.");
        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("ManagerData")]
        public IActionResult GetManagerData()
        {
            return Ok("Only Managers can access this.");
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        [Route("EmployeeData")]
        public IActionResult GetEmployeeData()
        {
            return Ok("Only Employees can access this.");
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("AdminOrManagerData")]
        public IActionResult GetManagerOrAdminData()
        {
            return Ok("Only Admins or Managers can access this.");
        }



    }

}
