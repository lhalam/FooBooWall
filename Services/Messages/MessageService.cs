using DataAccess.DAO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly MessageDAO dao = new MessageDAO();
        public List<Message> GetGlobal()
        {
            return dao.GetGlobal();
        }

        public List<Message> ReadAllPrivate(int userId, int? amount)
        {
            return dao.GetAllPrivate(userId, amount);
        }

        public List<Message> GetChat(int firstUserId, int secondUserId, int? amount)
        {
            return dao.GetChat(firstUserId, secondUserId, amount);
        }

        public void Send(Message message)
        {
            dao.Create(message);
        }
    }
}
