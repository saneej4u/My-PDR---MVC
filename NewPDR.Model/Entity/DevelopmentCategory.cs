using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class DevelopmentCategory
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public int UserTypeId { get; set; }

        public UserType UserType { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DevelopmentCategory()
        {
            DateTimeCreated = DateTime.Now;
        }

    }
}
