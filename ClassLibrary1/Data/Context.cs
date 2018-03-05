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
            Database.SetInitializer(new DatabaseInitializer());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
        }

        public DbSet<Surfboard> Surfboards { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<BoardStyle> BoardStyles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // TODO: may not use, since rating is now a float
            //modelBuilder.Entity<Surfboard>()
            //    .Property(sb => sb.Rating)
            //    .hasPrecision(4.2);
        }
    }
}
