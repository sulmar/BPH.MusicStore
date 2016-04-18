using BPH.MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPH.MusicStore.DAL.Configurations
{
    class ArtistConfiguration : EntityTypeConfiguration<Artist>
    {
        public ArtistConfiguration()
        {
            Property(p => p.FirstName)
              .HasMaxLength(50);


            Property(p => p.FirstName)
                .IsConcurrencyToken();

            Property(p => p.LastName)
                .IsConcurrencyToken();


        }
    }
}
