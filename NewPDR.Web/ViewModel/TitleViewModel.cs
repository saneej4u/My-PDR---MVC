using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class TitleViewModel
    {
        public PDReview MyPDR { get; set; }

        public List<PDReview> ColleaguesPDR { get; set; }

        public List<PDReview> AllPDRs { get; set; }

        public List<ApplicationUser> AllUsers { get; set; }

    }
}