using SurfboardQuiverConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SurfboardQuiverConsoleApp
{
    public class Context : DbContext
    {
        public Context()
        {
            //// Disable Initializers below on behalf of EF Migrations initializer
            //Database.SetInitializer(new DatabaseInitializer());

            Database.SetInitializer<Context>(null);

            ////Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
            ////Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
        }

        public DbSet<Surfboard> Surfboards { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<BoardStyle> BoardStyles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
