using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HKSupply.Models;

namespace HKSupply.DB
{
    public class HKSupplyContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Functionality> Functionalities { get; set; }

        public HKSupplyContext()
            : base("name=SqlExpressConn")
        {

        }

    }
}
