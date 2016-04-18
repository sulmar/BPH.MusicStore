using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPH.MusicStore.Models
{
    // [Table("Artysci")]
    public class Artist : Base
    {

        [Key]
        public int ArtistId { get; set; }

        // [MaxLength(50)]
        // [ConcurrencyCheck]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        // navigation property
        public virtual ICollection<Album> Albums { get; set; }

        public string FullName
        {
            get
            {
                // C# 6.0
                return $"Pan(i) {FirstName} {LastName}";
                    
                    // String.Format("Pan(i) {0} {2}", FirstName, LastName);
            }
         }


        public override string ToString()
        {
            return this.FullName;
        }

    }
}
