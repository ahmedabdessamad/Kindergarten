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
    public class Evenement
    {
        public int Id { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Required]
        public String Host { get; set; }

        [MaxLength(250)]
        [StringLength(250)]
        [Required]
        public String Description { get; set; }


        public string Subject { get; set; }
        [DataType(DataType.DateTime,ErrorMessage = "Date is not valid")]
        [Required]
        public DateTime Start { get; set; }
        [DataType(DataType.DateTime,ErrorMessage = "Date is not valid")]
        [Required]
        public DateTime End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        [MaxLength(125)]
        [StringLength(125)]
        [Required]
        public String Lieu { get; set; }
        [Required]
        public String Tel { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public String EmailContact { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string image { get; set; }
        [NotMapped]
        public HttpPostedFileBase file { get; set; }
    }
}
