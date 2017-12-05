using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class UserIndexLayoutVM
    {
        public PickVM[] Picks { get; set; }
        public MarketItemVM[] LatestMarketItems { get; set; }
    }
}
