using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class ProductComment
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }


        public Profil_Parent Parent { get; set; }

        [ForeignKey("Parent")]
        public int ParentId { get; set; }


        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
