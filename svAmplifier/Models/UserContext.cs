using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace svAmplifier.Models.Entities
{
    public partial class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> c) :base( c)
        {

        }

    }
}
