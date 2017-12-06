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
        public Mushroom Mushroom { get; set; }
        [Required]
        public double Price { get; set; }
        public string SalesPersonUsername { get; set; }
        public Address SalesAdress { get; set; }
        public DateTime PickDate { get; set; }
        public double Weight { get; set; }
    }
}
