using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class Rating
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public RatingType RatingType { get; set; }

        public int? RatingTypeId { get; set; }

        public int ScoreFrom { get; set; }

        public int ScoreTo { get; set; }

        public int ScoreIndex { get; set; }

        public int? UserTypeId { get; set; }

        public virtual  UserType  UserType{ get; set; }

        public DateTime DateTimeCreated { get; set; }

        public Rating()
        {
            DateTimeCreated = DateTime.Now;
        }
    }
}
