using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class RDV
    {

        public int Id { get; set; }

        public DateTime DateRdv { get; set; }
        public String LieuRdv { get; set; }
        public String MotifRdv { get; set; }
        

        [Key, Column(Order = 0)]
        [ForeignKey("Parent")]
        public int ParentFk { get; set; }
        public Profil_Parent Parent { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Directeur")]
        public int DirecteurFk { get; set; }
        public Profil_Directeur Directeur { get; set; }



    }
}
