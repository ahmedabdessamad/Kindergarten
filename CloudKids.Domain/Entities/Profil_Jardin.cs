using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Profil_Jardin : Profil
    {
        public Profil_Directeur Directeur { get; set; }

        [ForeignKey("Directeur")]
        public int DirecteurId { get; set; }

        public String Desciption { get; set; }

        public DateTime DateCreation { get; set; }

        public int NbreEmployees { get; set; }

        public String Localisation { get; set; }

        public Double Prix { get; set; }

       

        public virtual IEnumerable<Reclamation> Reclamations { get; set; }


        public virtual IEnumerable<Carte_Paiement> CartePaiements { get; set; }

    }
}
