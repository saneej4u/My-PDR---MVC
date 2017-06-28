using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class RatingType
    {
         [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public RatingType()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
