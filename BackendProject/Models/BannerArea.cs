using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class BannerArea
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }


    }
}
