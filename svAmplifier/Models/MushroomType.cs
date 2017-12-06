using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models
{
    public enum MushroomType
    {
        [Display(Name = "Kantarell")]
        Chanterelle,
        [Display(Name = "Karl Johan")]
        Karl_Johan,
        [Display(Name = "Champinjon")]
        White_Mushroom
    }
}
