using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseContext : DbContext
    {
        #region Properties
        public static string DatabaseConnectionString { get; set; }
        #endregion

        #region Constructor
        public BaseContext()
            : this(DatabaseConnectionString)
        {
        }

        public BaseContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<BaseContext>(null);

            // Disable lazy loading
            Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Do not pluralize relationships
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Stop using unicode for varchar strings comparisons (remove the N' in the comparisons)
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("VARCHAR"));
        }
    }
}
