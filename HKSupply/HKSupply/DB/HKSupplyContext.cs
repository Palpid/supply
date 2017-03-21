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
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierHistory> SuppliersHistory { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemHistory> ItemsHistory { get; set; }

        public HKSupplyContext()
            : base("name=SqlExpressConn")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set precision 
            modelBuilder.Entity<Item>().Property(x => x.MmFront).HasPrecision(19, 6);
            modelBuilder.Entity<Item>().Property(x => x.Caliber).HasPrecision(19, 6);

            modelBuilder.Entity<ItemHistory>().Property(x => x.MmFront).HasPrecision(19,6);
            modelBuilder.Entity<ItemHistory>().Property(x => x.Caliber).HasPrecision(19, 6);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Override del método original para que concatene todos los erroes del entity validation
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //Obtenemos los errores como una lista de string
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                //Unimos los errores en un solo string
                var fullErrorMessage = string.Join("; " + Environment.NewLine, errorMessages);

                //Combinamos la excepción original con el mensaje que hemos creado
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //Lanzamos un DbEntityValidationException con el mensaje de excepción que hemos montado
                throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            
        }

    }
}
