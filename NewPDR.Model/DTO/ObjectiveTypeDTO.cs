using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model.DTO
{
    public class ObjectiveTypeDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserTypeId { get; set; }

        public UserType UserType { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public int Index { get; set; }

    }
}
