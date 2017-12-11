using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class UserIndexLayoutVM
    {

        public Pick[] Picks { get; set; }
        public Pick[] LatestMarketItems { get; set; }
    }
}
