using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class FullYearChartViewModel
    {
        public string UserId { get; set; }

        public string ChartTitle { get; set; }

        public string Name { get; set; }

        public List<string> xValues { get; set; }

        public List<int> yValues { get; set; }

        public bool IsChartValueZeroFullYear { get; set; }
    }
}