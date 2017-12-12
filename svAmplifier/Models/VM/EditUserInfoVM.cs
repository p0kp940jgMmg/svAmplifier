using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
    public class EditUserInfoVM
    {

        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        public string Phonenumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Username { get; set; }

        public Pick[] Picks { get; set; }
    }
}
