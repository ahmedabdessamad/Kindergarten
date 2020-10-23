using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CloudKids.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Double Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateAddition { get; set; }

        public String imagePath { get; set; }

        public String imagePath1 { get; set; }

        public String imagePath2 { get; set; }

        public String imagePath3 { get; set; }

        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }

        [NotMapped]
        public HttpPostedFileBase imageFile1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile3 { get; set; }

        public Status StockStatus { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory Category { get; set; }
        public int? CategoryId { get; set; }


        public virtual IEnumerable<ProductComment> ProductComments { get; set; }


        public Profil_Directeur ProfilDirecteur { get; set; }

        [ForeignKey("ProfilDirecteur")]
        public int DirectorId { get; set; }

        public virtual IEnumerable<CartLine> CartLines { get; set; }


    }

    public enum Status
    {

    }
}
