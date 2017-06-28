using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewPDR.Web.ViewModel
{
    public class PDPViewModel
    {

        public int ID { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [Display(Name = "Development Need")]
        public string DevelopmentNeed { get; set; }


        [Display(Name = "Solution")]
        public string Solution { get; set; }

        [Display(Name = "Cost & Other Resource")]
        public string CostAndResource { get; set; }

        [Display(Name = "Time scale")]
        public string TimeScale { get; set; }

        public PDReview PDReview { get; set; }

        public IEnumerable<SelectListItem> DevelopmentCategorys { get; set; }
        [Display(Name = "Development Category")]
        public string SelectedDevelopmentCategory { get; set; }

        public int PDPIndexIndex { get; set; }

        public NewPDR.Service.WhoAmI WhoAmI { get; set; }

        public bool IsDisableWholeView { get; set; }


    }
}