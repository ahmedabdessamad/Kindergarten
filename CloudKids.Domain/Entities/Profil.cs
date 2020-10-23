using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Profil
    {
        [Key]
        public int Id { get; set; }

        public Statut Statut { get; set; }

        public DateTime Calendar { get; set; }

        public String Email { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        [NotMapped]
        public String ConfirmPassword { get; set; }

        public string Name { get; set; }

        public Gender? Gender { get; set; }

        public String Phone { get; set; }

        public String Image { get; set; }

        public Adresse Adresse { get; set; }
    }

    public enum Statut
    {
        Enabled,
        Disabled
    }

    public enum Gender
    {
        Homme,
        Femme,
        Other,
        PreferNotToSay
    }
}
