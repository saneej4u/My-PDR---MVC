using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.ViewModel
{
    public class ToDoListViewModel
    {
        public int ID { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        public string ToDoApplicationUserId { get; set; }
    }
}