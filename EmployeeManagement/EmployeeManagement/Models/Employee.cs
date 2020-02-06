using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "Name cannot exceed 80 characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [Display(Name="Office Email")]
        public string Email { get; set; }
        public Dept Department { get; set; }

    }
}
