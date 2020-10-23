using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Profil_Directeur : Profil
    {
        public virtual IEnumerable<Reunion> Reunions { get; set; }

        public virtual IEnumerable<RDV> Rdvs { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
