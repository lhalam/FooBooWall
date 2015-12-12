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
                UserId = GetUserId()
            };

            AddOrUpdateUserInDictionary(user);
            Clients.All.onlineUserCount(Users.Count);
            return base.OnConnected();
        }

        private void AddOrUpdateUserInDictionary(UserInfoHub user)
        {
            if (!Users.ContainsKey(user.Name))
            {
                Users.AddOrUpdate(user.Name, user, (key, oldValue) => user);
                 Clients.AllExcept(Context.ConnectionId).onNewUserConnected(user.UserId, user.Name);
            }

            lock (user.ConnectionsIdList)
            {
                user.ConnectionsIdList.Add(Context.ConnectionId);
            }
            Clients.Caller.onConnected(user.UserId, user.Name, UsersList);
        }

 

        public void SendPrivateMessage(string toUserName, string message)
        {

            string fromUserId = Context.ConnectionId;
            UserInfoHub toUser, fromUser;
            Users.TryGetValue(toUserName, out toUser);
            Users.TryGetValue(GetCurrentUserLoginName(), out fromUser);

            if (toUser != null && fromUser != null)
            {
                lock(toUser.ConnectionsIdList)
                {
                    foreach (var id in toUser.ConnectionsIdList)
                    {
                        Clients.Client(id).sendPrivateMessage(fromUser.Name, fromUser.Name, message);
                    }
                }
                
                lock(fromUser.ConnectionsIdList)
                {
                    foreach (var id in fromUser.ConnectionsIdList)
                    {
                        Clients.Client(id).sendPrivateMessage(fromUser.Name, toUser.Name, message);
                    }
                }
            }

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            UserInfoHub user;
            Users.TryGetValue(GetCurrentUserLoginName(), out user);
            if(user != null)
            {
                lock(user.ConnectionsIdList)
                {
                    if (user.ConnectionsIdList.Count > 1)
                    {
                        user.ConnectionsIdList.RemoveWhere(id => id.Equals(Context.ConnectionId));
                    }
                    else
                    {
                        UserInfoHub removedUser;
                        Users.TryRemove(GetCurrentUserLoginName(), out removedUser);
                        Clients.All.onUserDisconnected(GetUserId(), GetCurrentUserLoginName());
                    }
                }
            }
            
            Clients.All.onlineUserCount(Users.Count);
            return base.OnDisconnected(stopCalled);
        }

        private int GetUserId()
        {
            return Context.User.Identity.GetUserId<int>();
        }

        private string GetCurrentUserLoginName()
        {
            return Context.Request.User.Identity.Name;
        }


    }
}