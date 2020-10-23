using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Enfant
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public Profil_Parent Parent{ get; set; }

        [ForeignKey("Parent")]
        public int ParentId { get; set; }
    }
}
