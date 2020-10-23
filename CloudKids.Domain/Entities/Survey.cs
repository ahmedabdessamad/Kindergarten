using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace CloudKids.Domain.Entities
{
    public class Survey
    {
        public Survey()
        
        {
            Questions = new List<Question>();
            Responses = new List<Response>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<Question> Questions { get; set; }

        public List<Response> Responses { get; set; }

        public bool IsActive
        {
            get { return StartDate < DateTime.Now && EndDate > DateTime.Now; }
        }

        public string ToJson()
        {
            var js = JsonSerializer.Create(new JsonSerializerSettings());
            var jw = new StringWriter();
            js.Serialize(jw, this);
            return jw.ToString();
        }








    //    public Profil_Parent ProfilParent { get; set; }

    //   [ForeignKey("ProfilParent")]
      //  public int ProfilParentId { get; set; }


     //   public Profil_Jardin ProfilJardin { get; set; }

       // [ForeignKey("ProfilJardin")]
       // public int ProfilJardinId { get; set; }
        
    }

  
}
