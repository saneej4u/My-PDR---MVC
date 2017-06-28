using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Model
{
    public class ToDoList
    {
        public int ID { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public ToDoList()
        {
            DateTimeCreated = DateTime.Now;
        }

    }
}
