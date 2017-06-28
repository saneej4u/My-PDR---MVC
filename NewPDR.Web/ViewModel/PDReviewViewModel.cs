using NewPDR.Model;
using NewPDR.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class PDReviewViewModel
    {
        public int ID { get; set; }

        public DateTime ReviewPeriod { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string MidYearStatus { get; set; }

        public string MidYearStatusButton { get; set; }

        public string FullYearStatus { get; set; }

        public string FullYearStatusButton { get; set; }

        [Display(Name = "Colleague Overall Comments")]
        public string ColleagueOverallComments { get; set; }

         [Display(Name = "Colleague Signature: ")]
        public string ColleagueSigned { get; set; }

         [Display(Name = "Date: ")]
        public DateTime? ColleagueSignedDate { get; set; }

        [Display(Name = "Manager Overall Comments")]
        public string ManagerOverallComments { get; set; }

        public string ManagerSigned { get; set; }

        public DateTime? ManagerSignedDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public UserType UserType { get; set; }

        public int ManagerSignedDay { get; set; }

        public int ManagerSignedMonth { get; set; }

        public int ManagerSignedYear { get; set; }

        public NewPDR.Service.WhoAmI WhoAmI { get; set; }

        public CurrentPDRStatuses MidYearStatusE { get; set; }

        public CurrentPDRStatuses FullYearStatusE { get; set; }

        public string ReviewUserEmail { get; set; }

        public string LineMangerEmailId { get; set; }

        public string ManagerEmailId { get; set; }

        public bool IsManagerSigned { get; set; }

        public bool IsColleagueSigned { get; set; }

        public DateTime? LockedAt { get; set; }

        public DateTime? LockEndTime { get; set; }

        public string LockedBy { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDisableWholeView { get; set; }

        public Current_YearStatus  IsMidOrFullYear { get; set; }

    }
}