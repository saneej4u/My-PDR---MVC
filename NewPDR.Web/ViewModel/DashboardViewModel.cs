using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class DashboardViewModel
    {
        public string  ApplicationUserId { get; set; }

        public List<PDReviewViewModel> PDReviewViewModels { get; set; }

        public List< ColleaguesViewModel> Colleagues { get; set; }

        public List<ColleaguesViewModel> Others { get; set; }
    }
}