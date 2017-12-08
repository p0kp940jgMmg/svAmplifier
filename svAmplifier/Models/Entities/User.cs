using System;
using System.Collections.Generic;

namespace svAmplifier.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Pick = new HashSet<Pick>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string AspNetId { get; set; }
        public string Phonenumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }

        public ICollection<Pick> Pick { get; set; }
    }
}
