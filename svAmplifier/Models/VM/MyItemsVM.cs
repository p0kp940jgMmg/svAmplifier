using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class MyItemsVM
    {
        public Pick[] MyMarketItems { get; set; }
        public string Message { get; set; } = "Couldnt add ur pick";
        public Pick NewPick { get; set; }

        //public string Mushrooms { get; set; }
			
        //public string Weight { get; set; }
        //public string MushroomType { get; set; }
        //public string 
    }
}
