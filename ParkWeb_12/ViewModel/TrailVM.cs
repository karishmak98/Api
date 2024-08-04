using Microsoft.AspNetCore.Mvc.Rendering;
using ParkWeb_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_12.ViewModel
{
    public class TrailVM
    {
        public Trail Trail { get; set; }
        public IEnumerable<SelectListItem> nationalParkList { get; set; }
    }
}
