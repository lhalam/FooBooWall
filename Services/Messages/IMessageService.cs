using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messages
{
    public interface IMessageService
    {
        List<Message> GetGlobal();
        List<Message> ReadAllPrivate(int userId, int? amount = null);
        List<Message> GetChat(int firstUserId, int secondUserId, int? amount = null);
        void Send(Message message);
    }
}