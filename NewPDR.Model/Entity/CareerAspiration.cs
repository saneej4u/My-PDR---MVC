using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class CareerAspiration
    {
        public int ID { get; set; }

        public string PreferredNextMove { get; set; }

        public string TimingOfNextMove { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public CareerAspiration()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
