using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class PersonalDevelopmentPlan
    {

        public int ID { get; set; }

        public string DevelopmentNeed { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Solution { get; set; }

        public string CostAndResource { get; set; }

        public string TimeScale { get; set; }

        public int? PDReviewId { get; set; }

        public virtual PDReview PDReview { get; set; }

        public int DevelopmentCatId { get; set; }

        public virtual DevelopmentCategory DevelopmentCategory { get; set; }

        public PersonalDevelopmentPlan()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
