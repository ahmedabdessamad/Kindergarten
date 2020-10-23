using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudKids.Web.Models
{
    public class ComplexProductObject
    {
    
        // i use this model to create new object with product & cartline attributes for DetailsCart view

        public string ProductName { get; set; }

        public Double ProductPrice { get; set; }

        public int availableQuantity { get; set; }

        public String imagePath { get; set; }

        public DateTime CartDateAdded { get; set; }

        public int ChoosedQuantity { get; set; }

        public Double TotalCartLinePrice { get; set; }


    }
}