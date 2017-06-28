using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class MidYearChartViewModel
    {
        public string UserId { get; set; }

        public string ChartTitle { get; set; }

        public string Name { get; set; }

        public List<string> xValues { get; set; }

        public List<int> yValues { get; set; }

        public bool  IsChartValueZero { get; set; }

        public string ChartType { get; set; }
    }
}