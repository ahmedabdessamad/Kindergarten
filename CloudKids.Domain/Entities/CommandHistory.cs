using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class CommandHistory
    {
        [Key, ForeignKey("MyCart")] // Else Error // https://galdin.dev/blog/upper-bound-of-the-multiplicity-of-the-dependent-role-must-be-asterik/
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public Cart MyCart { get; set; }

        //[ForeignKey("MyCart")]
        //public int? CartId { get; set; }
    }
}
