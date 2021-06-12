using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string Adress { get; set; }

        [Required]
        public string FirstEmail { get; set; }
        public string SecondEmail { get; set; }
    }
}
