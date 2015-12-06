using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PmiOfficial.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PmiOfficial.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConcurrentDictionary<string, UserInfoHub> Users
                   = new ConcurrentDictionary<string, UserInfoHub>();

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

<<<<<<< HEAD
        // Подключение нового пользователя
        override public Task OnConnected()
=======
        public void Connect(string name, int userId)
>>>>>>> e69b3341a9f15008c7757579401cac5d21d4a2a2
        {
            var id = Context.ConnectionId;
            string name = GetCurrentUserLoginName();

<<<<<<< HEAD
            UserInfoHub user = new UserInfoHub
            {
                ConnectedTime = DateTime.Now,
                Name = name
            };
            AddOrUpdateUserInDictionary(user);
            Clients.Caller.userConnected(id, name, Users.Count);
            return Clients.All.onlineUserCount(Users.Count);
        }

        private void AddOrUpdateUserInDictionary(UserInfoHub user)
        {
            if (!Users.ContainsKey(user.Name))
            {
                Users.AddOrUpdate(user.Name, user, (key, oldValue) => user);
            }
            AddConnectionIdToUser(user);
        }

        private void AddConnectionIdToUser(UserInfoHub user)
        {
            if (Users.ContainsKey(user.Name))
            {
                Users[user.Name].ConnectionsIdList.Add(Context.ConnectionId);
=======
            if (!Users.Any(x => x.UserId == userId))
            {
                Users.Add(new UserInfoHub { ConnectionId = id, Name = name, UserId = userId });
                Clients.Caller.onConnected(id, name, Users);
                Clients.AllExcept(id).onNewUserConnected(id, name);
>>>>>>> e69b3341a9f15008c7757579401cac5d21d4a2a2
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
<<<<<<< HEAD
            if (stopCalled = true && Users[GetCurrentUserLoginName()].ConnectionsIdList.Count > 1)
            {
                DeleteCurrentConnectionIdInList();
            }
            else
            {
                UserInfoHub removedUser;
                Users.TryRemove(GetCurrentUserLoginName(), out removedUser);
=======
            if (stopCalled)
            {
                var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    Users.Remove(item);
                    var id = Context.ConnectionId;
                    Clients.All.onUserDisconnected(id, item.Name);
                }
>>>>>>> e69b3341a9f15008c7757579401cac5d21d4a2a2
            }
            return Clients.All.onlineUserCount(Users.Count, Users);
        }

        private void DeleteCurrentConnectionIdInList()
        {
            int indexOfCurrentConnectionId = Users[GetCurrentUserLoginName()]
                .ConnectionsIdList.FindIndex(s => s.Contains(Context.ConnectionId));

            Users[GetCurrentUserLoginName()].ConnectionsIdList.RemoveAt(indexOfCurrentConnectionId);
        }

        private string GetCurrentUserLoginName()
        {
            return Context.Request.User.Identity.Name;
        }


    }
}