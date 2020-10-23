using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Profil_Parent : Profil
    {
        //[Key]
       // public int ProfilParentId { get; set; }
        public virtual IEnumerable<Enfant> Enfants { get; set; }

      //  public virtual IEnumerable<Survey> Surveys { get; set; }

        public virtual IEnumerable<Reclamation> Reclamations { get; set; }

        public virtual IEnumerable<RDV> Rdv { get; set; }

        public virtual IEnumerable<Reunion> Reunions { get; set; }

        public virtual IEnumerable<ProductComment> ProductComments { get; set; }


       

    }
}
