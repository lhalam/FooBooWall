using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PmiOfficial.Models;

namespace PmiOfficial.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserInfoHub> Users = new List<UserInfoHub>();

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public void Connect(string name, int userId)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.UserId == userId))
            {
                Users.Add(new UserInfoHub { ConnectionId = id, Name = name, UserId = userId });
                Clients.Caller.onConnected(id, name, Users);
                Clients.AllExcept(id).onNewUserConnected(id, name);
            }
        }

        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = Users.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = Users.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.Name, message);
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.Name, message);
            }

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    Users.Remove(item);
                    var id = Context.ConnectionId;
                    Clients.All.onUserDisconnected(id, item.Name);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}