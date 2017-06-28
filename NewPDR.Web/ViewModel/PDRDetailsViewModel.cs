using FluentValidation;
using FluentValidation.Attributes;
using NewPDR.Web.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.ViewModel
{

    [Validator(typeof(PDRDetailsViewModelValidator))]
    public class PDRDetailsViewModel
    {
        public int PDRId { get; set; }
        public DateTime ReviewPeriod { get; set; }
        public string CurrentPDRStatus { get; set; }
        public bool IsDisableWholeView { get; set; }
        public NewPDR.Service.WhoAmI WhoAmI { get; set; }
        public PDReviewViewModel PDReviewViewModel { get; set; }
        public IList<ObjectiveViewModel> Objectives { get; set; }
        public IList<SuccessFactorViewModel> SuccessFactors { get; set; }
        public IList<PDPViewModel> PDPlans { get; set; }


        [Display(Name = "Overall rating for objectives")]
        public string SelectedObjectiveOverallRatingId { get; set; }
        public IEnumerable<SelectListItem> ObjectiveOverallRatings { get; set; }

        [Display(Name = "Overall rating for success factor")]
        public string SelectedSuccessFactorOverallRatingId { get; set; }
        public IEnumerable<SelectListItem> SuccessFactorOverallRatings { get; set; }

        [Display(Name = "Anuual overall ratings")]
        public string SelectedOverallRatingId { get; set; }
        public IEnumerable<SelectListItem> OverallRatings { get; set; }

        public string AccessRequest { get; set; }

        public string AccessResponse { get; set; }

        public string AcessRequestedUserId { get; set; }

        public bool IsLockedByCurrentUser { get; set; }

        public string LockedBy { get; set; }

        public DateTime LockedAt { get; set; }

        public DateTime? EndLocked { get; set; }

        public bool IsAnyAccessRequest { get; set; }
   
        public bool IsAnyAccessResponse { get; set; }

        public bool IsReadyToCloseMidYear { get; set; }

        public bool IsReadyToCloseFullYear { get; set; }

    }

}