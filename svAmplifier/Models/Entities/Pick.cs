using svAmplifier.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.Entities
{
    public class Pick
    {
        public int Id { get; set; }
        public Mushroom Mushroom { get; set; }
        public User User { get; set; }
        public Location Location { get; set; }
    }
}
