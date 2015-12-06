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

        // Отправка сообщений
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
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled = true && Users[GetCurrentUserLoginName()].ConnectionsIdList.Count > 1)
            {
                DeleteCurrentConnectionIdInList();
            }
            else
            {
                UserInfoHub removedUser;
                Users.TryRemove(GetCurrentUserLoginName(), out removedUser);
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