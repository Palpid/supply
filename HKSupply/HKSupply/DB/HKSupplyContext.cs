using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HKSupply.Models;

namespace HKSupply.DB
{
    /// <summary>
    /// Contexto de base de datos Entity Framework
    /// </summary>
    public class HKSupplyContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<FunctionalityRole> FunctionalitiesRole { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerHistory> CustomersHistory { get; set; }

        public HKSupplyContext()
            : base("name=SqlExpressConn")
        {

        }

    }
}
