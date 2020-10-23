using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Publication
    {
        public int Id { get; set; }

        public String Contenu { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual IEnumerable<Commentaire> Commentaires { get; set; }

        public virtual IEnumerable<Appreciation> Appreciations { get; set; }
    }
}
