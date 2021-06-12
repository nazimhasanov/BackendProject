using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Slider { get; set; }

        public VideoTour Video { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }

    }
}
