using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Reclamation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required.")]
        public String Motif { get; set; }


        public Profil_Jardin ProfilJardin { get; set; }

        [ForeignKey("ProfilJardin")]
        public int ProfilJardinId { get; set; }


        public Profil_Parent ProfilParent { get; set; }
        
        [ForeignKey("ProfilParent")]
        public int ProfilParentId { get; set; }

    }
}
