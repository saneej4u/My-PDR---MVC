using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class ActivityViewModel
    {
        public int ID { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Name { get; set; }

        public string ApplicationUserId { get; set; }
    }
}