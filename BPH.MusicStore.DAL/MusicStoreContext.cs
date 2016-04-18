using BPH.MusicStore.DAL.Configurations;
using BPH.MusicStore.DAL.Conventions;
using BPH.MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPH.MusicStore.DAL
{
    public class MusicStoreContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }

        public DbSet<Album> Albums { get; set; }

        public MusicStoreContext()
            : base("MusicStoreConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("sales");


            this.Database.Log += msg => System.Diagnostics.Debug.WriteLine(msg);

            #region Configurations

            modelBuilder.Configurations.Add(new AlbumConfiguration());
            modelBuilder.Configurations.Add(new ArtistConfiguration());

            #endregion

            #region Conventions

            modelBuilder.Conventions.Add(new DateTime2Convention());

            #endregion

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;


            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {

            

            Console.WriteLine("Zapisywanie danych...");

            // Sprawdzamy czy są jakieś zmienione encje
            if (this.ChangeTracker.HasChanges())
            {
                var entries = this.ChangeTracker.Entries();


                var added = this.ChangeTracker.Entries<Base>()
                    .Where(e => e.State == EntityState.Added)
                    .Select(e => e.Entity)
                    .ToList();

                var modified = this.ChangeTracker.Entries<Base>()
                   .Where(e => e.State == EntityState.Modified)
                   .Select(e => e.Entity)
                   .ToList();

                foreach (var item in modified)
                {
                    item.ModifiedDate = DateTime.Now;
                }

                added.ForEach(item => item.CreatedDate = DateTime.Now);


                var deleted = this.ChangeTracker.Entries<Base>()
                   .Where(e => e.State == EntityState.Deleted)
                   .Select(e => e.Entity)
                   .ToList();



                //foreach (var entry in added)
                //{
                //    entry.
                //}

            }

            return base.SaveChanges();
        }



    }
}
