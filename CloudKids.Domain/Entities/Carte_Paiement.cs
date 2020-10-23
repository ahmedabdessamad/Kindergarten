using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Carte_Paiement
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required.")]
        public CarteType Type { get; set; }

        [Required(ErrorMessage = "This Field Is Required.")]
        [StringLength(25, ErrorMessage = "Max  Length 12")]
        [MaxLength(50, ErrorMessage = "Max Length is 12")]
        public String Numero { get; set; }


       
        public DateTime DateExpiration { get; set; }

        public String Pays { get; set; }

        public String Adresse { get; set; }

        public String Ville { get; set; }

        public String Region { get; set; }

        public int CodePostal { get; set; }

        //[Key, Column(Order = 0)]
        //[ForeignKey("Owner")]
        //public int ParentFk { get; set; }

        //public Profil_Parent Owner { get; set; }


        //[Key, Column(Order = 1)]
        //[ForeignKey("PaidJardin")]
        //public int JardinFk { get; set; }

        //public Profil_Jardin PaidJardin{ get; set; }


    }

    public enum CarteType
    {
        Visa,
        Edinar,
        MasterCard
    }
}
