using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using svAmplifier.Models.Entities;

namespace svAmplifier.Models
{
    public class Clan
    {
        public string Name { get; set; }
        public User[] Users { get; set; }
    }
}
