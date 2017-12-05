using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using svAmplifier.Models.Entities;

namespace svAmplifier.Models
{
    public class AccountRepository
    {
        static List<User> users = new List<User>
        {
            new User {
                UserName = "Danne",
                Email = "danne@danne.com",
                Password = "password",
                Address = new Address
                {
                    City = "Sthlm",
                    Street = "Hornstull",
                    Zipcode = "12344"
                }
            },
            new User {
                UserName = "Pontus",
                Email = "pontus@pontus.com",
                Password = "password",
                Address = new Address
                {
                    City = "Sthlm",
                    Street = "Kistagatan",
                    Zipcode = "16533"
                }
            },
            new User {
                UserName = "Håkan",
                Email = "hakan@hakan.com",
                Password = "password",
                Address = new Address
                {
                    City = "Sollentuna",
                    Street = "Sollentunagatan",
                    Zipcode = "12452"
                }
            },
            new User {
                UserName = "Cristian",
                Email = "cristian@cristian.com",
                Password = "password",
                Address = new Address
                {
                    City = "Solna",
                    Street = "Solnagatan",
                    Zipcode = "1113"
                }
            },
            new User {
                UserName = "Patrick",
                Email = "patrik@patrik.com",
                Password = "password",
                Address = new Address
                {
                    City = "Malmö",
                    Street = "Malmögatan",
                    Zipcode = "22344"
                }
            }
        };

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User[] GetAllUsers()
        {
            return users.ToArray();
        }
    }
}
