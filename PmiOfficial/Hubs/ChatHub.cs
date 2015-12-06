using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PmiOfficial.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PmiOfficial.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConcurrentDictionary<string, UserInfoHub> Users
                   = new ConcurrentDictionary<string, UserInfoHub>();

        static IEnumerable<UserInfoHub> UsersList 
        {
            get
            {
                return Users.Values.ToList();
            }
        }

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }


        // Подключение нового пользователя
        override public Task OnConnected()
        {
            var id = Context.ConnectionId;
            string name = GetCurrentUserLoginName();


            UserInfoHub user = new UserInfoHub
            {
                ConnectedTime = DateTime.Now,
                Name = name,
                UserId = Context.User.Identity.GetUserId<int>()
            };

            AddOrUpdateUserInDictionary(user);
            return Clients.All.onlineUserCount(Users.Count);
        }

        private void AddOrUpdateUserInDictionary(UserInfoHub user)
        {
            if (!Users.ContainsKey(user.Name))
            {
                Users.AddOrUpdate(user.Name, user, (key, oldValue) => user);
                 Clients.AllExcept(Context.ConnectionId).onNewUserConnected(user.UserId, user.Name);
            }
            AddConnectionIdToUser(user);
        }

        private void AddConnectionIdToUser(UserInfoHub user)
        {
            if (Users.ContainsKey(user.Name))
            {
                Users[user.Name].ConnectionsIdList.Add(Context.ConnectionId);
            }

            Clients.Caller.onConnected(user.UserId, user.Name, UsersList);
        }

        public void SendPrivateMessage(string toUserName, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = Users[toUserName];
            var fromUser = Users[GetCurrentUserLoginName()];

            if (toUser != null && fromUser != null)
            {
                foreach (var id in toUser.ConnectionsIdList)
                {
                    Clients.Client(id).sendPrivateMessage(fromUser.UserId, fromUser.Name, message);
                }

                foreach (var id in fromUser.ConnectionsIdList)
                {
                    Clients.Client(id).sendPrivateMessage(toUser.UserId, fromUser.Name, message);
                }
            }

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            if (Users[GetCurrentUserLoginName()].ConnectionsIdList.Count > 1)
            {
                DeleteCurrentConnectionIdInList();
            }
            else
            {
                UserInfoHub removedUser;
                Users.TryRemove(GetCurrentUserLoginName(), out removedUser);

            }
            Clients.All.onUserDisconnected(Context.ConnectionId, GetCurrentUserLoginName());
            return Clients.All.onlineUserCount(Users.Count);
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