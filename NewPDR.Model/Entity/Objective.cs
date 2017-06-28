using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class Objective
    {
        public int ID { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string ColleagueComments  { get; set; }

        public string ManagerComments { get; set; }

        public string ColleagueEvidence { get; set; }

        public string ManagerEvidence { get; set; }

        public int MidYearRating { get; set; }

        public int FullYearRating { get; set; }

        public string  Description { get; set; }

        public int? PDReviewId { get; set; }

        public virtual PDReview PDReview { get; set; }

        public int ObjectiveTypeId { get; set; }

        public virtual  ObjectiveType ObjectiveType { get; set; }

        public Objective()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
