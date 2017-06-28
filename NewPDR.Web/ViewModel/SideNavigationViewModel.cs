using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class SideNavigationViewModel
    {
        public List<PDReview> MyPDRs { get; set; }
        public List<PDReview> MyteamsPDRs { get; set; }
        public List<PDReview> OthersPDR { get; set; }

    }
}