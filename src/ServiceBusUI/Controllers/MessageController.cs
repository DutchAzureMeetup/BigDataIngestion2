using System.Web.Http;
using Microsoft.AspNet.SignalR;
using ServiceBusUI.Models;

namespace ServiceBusUI.Controllers
{
    public class MessageController : ApiController
    {
        public IHttpActionResult Post(Message message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();

            context.Clients.All.newMessage(message.Body);

            return Ok();
        }
    }
}
