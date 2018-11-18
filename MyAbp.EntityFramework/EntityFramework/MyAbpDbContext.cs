using System.Data.Common;
using Abp.EntityFramework;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyAbp.Shopping;

namespace MyAbp.EntityFramework
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MyAbpDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        public virtual DbSet<Products> Products { get; set; }

        public virtual DbSet<ProductModels> ProductModels { get; set; }

        public virtual DbSet<Orders> Orders { get; set; }


        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MyAbpDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MyAbpDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MyAbpDbContext since ABP automatically handles it.
         */
        public MyAbpDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public MyAbpDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public MyAbpDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ColumnConfigurations(modelBuilder);
            RelationshipConfiguration(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private void ColumnConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Products.ProductsConfiguration());
        }

        private void RelationshipConfiguration(DbModelBuilder modelBuilder)
        {
            new Products.ProductsToOrdersMapping().AddModelBuilder(modelBuilder);
        }
    }
}
