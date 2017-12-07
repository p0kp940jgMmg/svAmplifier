﻿using System;
using System.Collections.Generic;

namespace svAmplifier.Models.Entities
{
    public partial class Pick
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MushroomName { get; set; }
        public string MushroomPicUrl { get; set; }
        public DateTime DatePicked { get; set; }
        public int? WeightInGrams { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
