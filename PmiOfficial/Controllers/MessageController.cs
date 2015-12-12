using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.DAO;
using DataAccess.Entities;
using PmiOfficial.Models;
using Services;
using Services.Messages;
using DataAccess.Entities;

namespace PmiOfficial.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        private IMessageService _messageService;
        private IUserService _userService;

        public MessageController()
        {
            _messageService = new MessageService();
            _userService = new UserService(new UserDAO());
        }

        [Route("getGlobal")]
        public IEnumerable<GlobalMessageViewModel> GetGlobal()
        {
            return _messageService.GetGlobal().Select(m =>
                new GlobalMessageViewModel
                {
                    SenderName = _userService.Get(m.AuthorId).Login,
                    Text = m.Text
                });
        }

        [Route("getPrivate")]
        public IEnumerable<MessageViewModel> GetPrivate(string senderName, string recepientName)
        {
            User sender = _userService.GetByLoginName(senderName);
            User recepient = _userService.GetByLoginName(recepientName);
            if (sender == null || recepient == null)
            {
                throw new ArgumentNullException("User with such login not exist!");
            }
            int senderId = sender.Id;
            int recepientId = recepient.Id;
            return _messageService.GetChat(senderId, recepientId).Select(m =>
                new MessageViewModel
                {
                    SenderName = m.AuthorId == senderId ? sender.Login : recepient.Login,
                    RecepientName = m.AuthorId == senderId ? sender.Login : recepient.Login,
                    Text = m.Text
                });
        }

        [HttpPost]
        [Route("sendPrivate")]
        public IHttpActionResult SendPrivate(MessageViewModel message)
        {
            User sender = _userService.GetByLoginName(message.SenderName);
            User recepient = _userService.GetByLoginName(message.RecepientName);
            if (sender == null || recepient == null)
            {
                return BadRequest("User with such login not exist!");
            }
            int senderId = sender.Id;
            int recepientId = recepient.Id;
            Message newMessage = new Message
            {
                AuthorId = senderId,
                RecipientId = recepientId,
                Text = message.Text,
                Time = DateTime.Now
            };
            _messageService.Send(newMessage);
            return Ok();
        }

        [HttpPost]
        [Route("sendGlobal")]
        public IHttpActionResult SendGlobal(GlobalMessageViewModel message)
        {
            User sender = _userService.GetByLoginName(message.SenderName);
            if (sender == null)
            {
                return BadRequest("User with such login not exist!");
            }
            int senderId = sender.Id;
            Message newMessage = new Message
            {
                AuthorId = senderId,
                RecipientId = null,
                Text = message.Text,
                Time = DateTime.Now
            };
            _messageService.Send(newMessage);
            return Ok();
        }
    }
}
