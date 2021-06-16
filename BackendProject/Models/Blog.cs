using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Blog
    {


        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public BlogDetail Blogdetail {get; set;}

        [NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }


    }
}
