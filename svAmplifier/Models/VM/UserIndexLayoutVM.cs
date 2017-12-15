using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class UserIndexLayoutVM
    {
        public Pick[] Picks { get; set; }
        public Pick[] LatestMarketItems { get; set; }
        public MyItemsVM MyItems { get; set; } = new MyItemsVM();
        public string Cordinates { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
