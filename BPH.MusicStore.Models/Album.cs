using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPH.MusicStore.Models
{
    public class Album : Base, IDisposable
    {
        public int AlbumId { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public AlbumType AlbumType { get; set; }

        public string CoverUrl { get; set; }

        public Artist Artist { get; set; }

        public string Genre { get; set; }



        //[Timestamp]

        public byte[] RowVersion { get; private set; }

        public void Dispose()
        {
            Console.WriteLine("czyszczenie");

        }
    }


}
