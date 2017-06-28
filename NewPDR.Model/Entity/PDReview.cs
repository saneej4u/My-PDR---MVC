using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class PDReview
    {
        public int ID { get; set; }

        public string ReviewerUserId { get; set; }

        public string ReviewerEmailId { get; set; }

        public DateTime ReviewPeriod { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public int MidYearStatus { get; set; }

        public int FullYearStatus { get; set; }

        public string ApplicationUserId { get; set; }

        public DateTime? LockedAt { get; set; }

        public DateTime? LockEndTime { get; set; }

        public string LockedBy { get; set; }

        public bool IsLocked { get; set; }

        public virtual ApplicationUser  ApplicationUser { get; set; }

        public int UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }

        public virtual ICollection<Objective> Objectives { get; set; }

        public virtual ICollection<SuccessFactor> SuccessFactors { get; set; }

        public int? ObjectiveOverallRatingId { get; set; }

        public int? SuccessFactorOverallRatingId { get; set; }

        public int? OverallRatingId { get; set; }

        public bool IsMidYear { get; set; }

        public bool IsFullYear { get; set; }

        //Start - Temp to calculate/monitor performance
        public int? MidyearObjectiveOverallScore { get; set; }

        public int? MidYearSuccessFactorOverallScore { get; set; }

        public int? MidYearOverallScore { get; set; }

        public int? FullYearObjectiveOverallScore { get; set; }

        public int? FullYearSuccessFactorOverallScore { get; set; }

        public int? FullYearOverallScore { get; set; }

        //End - Temp to calculate/monitor performance

        public string ColleagueOverallComments { get; set; }

        public string ColleagueSigned { get; set; }

        public DateTime? ColleagueSignedDate { get; set; }

        public string ManagerOverallComments { get; set; }

        public string ManagerSigned { get; set; }

        public DateTime? ManagerSignedDate { get; set; }

        public string AccessRequest { get; set; }

        public string AccessResponse { get; set; }

        public string AcessRequestedUserId { get; set; }

        public string LineManagerEmailId { get; set; }

        public PDReview()
        {
            DateTimeCreated = DateTime.Now;
            ReviewPeriod = DateTime.Today;
        }
    }
}
