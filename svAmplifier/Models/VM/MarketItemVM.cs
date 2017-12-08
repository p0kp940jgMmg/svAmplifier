using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class MarketItemVM
    {
        [Required]
        public string MushroomName { get; set; }
        [Required]
        public string MushroomPicUrl { get; set; }
        [Required]
        public double Price { get; set; }
        public string SalesPersonUsername { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public DateTime PickDate { get; set; }
        public double Weight { get; set; }
    }
}
