using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewPDR.Model
{
    public class SuccessFactorType
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public int Index { get; set; }

        public int? UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }

        public SuccessFactorType()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
