﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class PickVM
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MushroomName { get; set; }
        public string MushroomPicUrl { get; set; }
        public double Weight { get; set; }
        public DateTime PickDate { get; set; }
        public int UserId { get; set; }
    }
}
