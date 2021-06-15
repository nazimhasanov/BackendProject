using BackendProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject
{
    public class User : IdentityUser
    {
        [Required]
        public string Fullname { get; set; }        
        public string Surname { get; set; }
        public bool IsDeleted { get; set; }
        public List<Course> Courses { get; set; }
    }
}
