using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Information_System_ASP.Net.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        [MaxLength(100)]  // Limits the maximum length of Name to 100 characters
        public string Name { get; set; }


        [Required]
        [MaxLength(50)]  // Limits the maximum length of Role to 50 characters
        public string Role { get; set; }  // "Admin" or "Employee"
    }
}
