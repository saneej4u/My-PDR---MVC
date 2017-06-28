using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace NewPDR.Web
{
    public class AccessRequestHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.User(name).addNewMessageToPage(name, message);
  
        }
    }
}