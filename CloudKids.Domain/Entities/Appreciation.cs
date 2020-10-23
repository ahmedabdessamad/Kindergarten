using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudKids.Domain.Entities
{
    public class Appreciation
    {
        public int Id { get; set; }

        public AppreciationType Type { get; set; }

        public Publication Publication { get; set; }
        [ForeignKey("Publication")]
        public int PublicationId { get; set; }

    }

    public enum AppreciationType
    {
        Like,
        Dislike
    }
}
