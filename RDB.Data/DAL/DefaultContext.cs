using MySql.Data.EntityFramework;
using RDB.Data.Models;
using System;
using System.Data.Entity;

namespace RDB.Data.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DefaultContext : DbContext
    {
        #region Properties

        public DbSet<Bus> Buses { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<Drive> Drives { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        #endregion

        #region Constructors

        public DefaultContext() : base("DefaultContext") { }

        #endregion

        #region Protected methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>()
                .HasRequired(r => r.DepartureLocation)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasRequired(r => r.ArrivalLocation)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .HasRequired(t => t.Drive)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
