using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class NewPickVM
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MushroomName { get; set; }
        public DateTime DatePicked { get; set; }
        public int? WeightInGrams { get; set; }
        public bool? SalesItem { get; set; }
    }
}
