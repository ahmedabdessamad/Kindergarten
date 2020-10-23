using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Commentaire // Cascade Delete
    {
        public int Id { get; set; }

        public String Contenu { get; set; }

        public DateTime DateAdded { get; set; }

        public Publication Publication { get; set; }

        [ForeignKey("Publication")]
        public int PublicationId { get; set; }


    }
}
