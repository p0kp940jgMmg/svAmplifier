using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class PickVM
    {
        public Location PickLocation { get; set; }
        public Mushroom PickedMushroom { get; set; }
        public double Weight { get; set; }
        public DateTime PickDate { get; set; }
        public int UserId { get; set; }
    }
}
