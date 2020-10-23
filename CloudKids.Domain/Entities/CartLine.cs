using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class CartLine
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int Quantity { get; set; }

        public Double Price { get; set; }


        public virtual Product MyProduct { get; set; }

        [ForeignKey("MyProduct")]
        public int ProductId { get; set; }


        public virtual Cart MyCart { get; set; }

        [ForeignKey("MyCart")]
        public int CartId { get; set; }

        // I use this method to test if the cartLine is empty or not in the view DetailCart   @if (Model.Empty)

        public bool Empty
        {
            get
            {
                return (Id == 0);
            }

        }

    }
}
