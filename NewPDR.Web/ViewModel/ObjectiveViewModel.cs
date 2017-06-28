using NewPDR.Model;
using NewPDR.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.ViewModel
{
    public class ObjectiveViewModel
    {
        public int ID { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [Display(Name = "Colleague Comments")]
        public string ColleagueComments { get; set; }


        [Display(Name = "Manager Comments")]
        public string ManagerComments { get; set; }

        [Display(Name = "Colleague Evidence")]
        public string ColleagueEvidence { get; set; }

        [Display(Name = "Manager Evidence")]
        public string ManagerEvidence { get; set; }

        public PDReview PDReview { get; set; }

        public IEnumerable<SelectListItem> MidYearRatings { get; set; }
        [Display(Name = "Mid Year Ratings")]
        public string SelectedMidYearRating { get; set; }

        public IEnumerable<SelectListItem> FullYearRatings { get; set; }
        [Display(Name = "Full Year Ratings")]
        public string SelectedFullYearRating { get; set; }

        public ObjectiveType ObjectiveType { get; set; }

        public string SuccessMessage { get; set; }

        public int ObjectiveIndex { get; set; }

        public string Description { get; set; }

        public NewPDR.Service.WhoAmI WhoAmI { get; set; }

        public  CurrentPDRStatuses MidYearStatus { get; set; }

        public CurrentPDRStatuses FullYearStatus { get; set; }

        public bool IsDisableWholeView { get; set; }
    }
}