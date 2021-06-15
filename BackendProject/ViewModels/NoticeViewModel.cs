using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class NoticeViewModel
    {
        public ICollection<NoticeBoard> NoticeBoards { get; set; }
        public VideoTour VideoTour { get; set; }
    } 
}
