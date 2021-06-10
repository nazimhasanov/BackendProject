using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<Teacher> Teachers { get; set; }




    }
}
