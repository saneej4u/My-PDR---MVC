using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class SuccessFactor
    {

        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string ColleagueComments { get; set; }

        public string ManagerComments { get; set; }

        public string Strengths { get; set; }

        public string Improvements { get; set; }

        public int MidYearRating { get; set; }

        public int FullYearRating { get; set; }

        public int? PDReviewId { get; set; }

        public virtual PDReview PDReview { get; set; }

        public int SuccessFactorTypeId { get; set; }

        public virtual SuccessFactorType SuccessFactorType { get; set; }

        public SuccessFactor()
        {
            DateTimeCreated = DateTime.Now;
        }

    }
}
