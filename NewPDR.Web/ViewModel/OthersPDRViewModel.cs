using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class OthersPDRViewModel
    {
        public ApplicationUser ColleagueUser { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserNameOrEmail { get; set; }

        public string UserFrom { get; set; }

        public List<PDReview> PDReviews { get; set; }
    }
}