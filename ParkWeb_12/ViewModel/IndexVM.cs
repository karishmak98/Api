using ParkWeb_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_12.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<NationalPark> NationalParkList { get; set; }
        public IEnumerable<Trail> TrailList { get; set; }
    }
}
