using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
   public class HRProPersonnelRecord
    {
        public DateTime DateTimeCreated { get; set; }

        public string JobTitle { get; set; }

        public string  ForeName { get; set; }

        public string Surname { get; set; }

        public string  Department { get; set; }

        public string Division { get; set; }

        public string ManagerEmailId { get; set; }

        public string LineManagerEmailId { get; set; }

        [Key]
        public string EmailId { get; set; }

        public HRProPersonnelRecord()
        {
            DateTimeCreated = DateTime.Now;

        }
    }
}
