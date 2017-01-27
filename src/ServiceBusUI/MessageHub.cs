using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ServiceBusUI
{
    public class MessageHub : Hub
    {
        public void Send(string messagePayload)
        {
            Clients.All.newMessage(messagePayload);
        }
    }
}