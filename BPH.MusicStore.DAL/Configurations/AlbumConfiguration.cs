using BPH.MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPH.MusicStore.DAL.Configurations
{
    class AlbumConfiguration : EntityTypeConfiguration<Album>
    {
        public AlbumConfiguration()
        {
            HasKey(p => p.AlbumId);

            Property(p => p.Title)
                .HasMaxLength(200);

            Property(p => p.CoverUrl)
                .HasMaxLength(250);


            Property(p => p.RowVersion)
                .IsRowVersion();
        }
    }
}
