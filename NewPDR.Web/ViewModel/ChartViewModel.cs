using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class ChartViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<MidYearChartViewModel> MidYearChartViewModels { get; set; }
        public List<FullYearChartViewModel> FullYearChartViewModels { get; set; }
    }
}