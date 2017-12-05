using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using svAmplifier.Models.Entities;

namespace svAmplifier.Models
{
    public class AccountRepository
    {
        public User[] userList;

        public AccountRepository()
        {
            userList = new User[]
            {
                new User
                {
                    UserName = "Daniel",
                    Clan = "",
                    Address = ""
                },
                new User
                {
                    UserName = "Cristian",
                    Clan = "",
                    Address = ""
                },
                new User
                {
                    UserName = "Pontus",
                    Clan = "",
                    Address = ""
                }
            };
        }
    }
}
