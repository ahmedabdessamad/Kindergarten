using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Profil_Administrateur: Profil
    {
       



        [Required(ErrorMessage = "This Field Is Required.")]
        [Range(25,55,ErrorMessage="Age between 25 - 55")]
        public int Age { get; set; }

     


        


    }
}
