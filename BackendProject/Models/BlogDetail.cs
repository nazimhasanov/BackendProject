using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class BlogDetail
    {
        public int Id  { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public bool isDelete { get; set; }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }


    }
}
