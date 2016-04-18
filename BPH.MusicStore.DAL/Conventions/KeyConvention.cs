using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPH.MusicStore.DAL.Conventions
{
    class KeyConvention : Convention
    {
        public KeyConvention()
        {
            this.Properties<int>()
                .Where(p=>p.Name.EndsWith("Key"))
                .Configure(c=>c.IsKey());
                
        }
    }
}
