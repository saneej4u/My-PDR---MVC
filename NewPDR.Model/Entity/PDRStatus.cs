using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class PDRStatus
    {

        [DatabaseGenerated( DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Descriptions { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public PDRStatus()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
