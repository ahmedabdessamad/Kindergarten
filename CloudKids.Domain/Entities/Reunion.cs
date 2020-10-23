using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Reunion
    {

        public int Id { get; set; }

        public DateTime DateReunion { get; set; }
        public String LieuReunion { get; set; }
        public String MotifReunion { get; set; }


        public virtual IEnumerable<Profil_Parent> ProfilParent { get; set; }

        public Profil_Directeur ProfilDirecteur { get; set; }

        [ForeignKey("ProfilDirecteur")]
        public int ProfilDirecteurId { get; set; }




    }
}
