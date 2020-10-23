using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Reputation
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "This Field Is Required.")]
        [StringLength(25, ErrorMessage = "Max 10")]
        [MaxLength(50, ErrorMessage = "Max Length is 100")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Required(ErrorMessage = "This Field Is Required.")]
        public ReputationType Type { get; set; }

        public Double Prix { get; set; }

        public Profil_Jardin ProfilJardin { get; set; }

        [ForeignKey("ProfilJardin")]
        public int ProfilJardinId { get; set; }
    }

    public enum ReputationType
    {
        Gold,
        Silver,
        Bronze
    }
}
