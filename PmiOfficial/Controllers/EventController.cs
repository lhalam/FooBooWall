using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using DataAccess.DAO;
using Services.DTO;
using PmiOfficial.Models;
using Microsoft.AspNet.Identity;
using PmiOfficial.Filters;

namespace PmiOfficial.Controllers
{
    public class EventController : Controller
    {
        private EventService _eventService = new EventService(new EventDAO());
        private CommentService _commentsService = new CommentService(new EventCommentsDao());
        private static int TEST_EVENT_ID = 1;

        public ActionResult Index(int? userId, int? eventId) 
        {
            int idOfEvent = eventId == null ? TEST_EVENT_ID : (int)eventId;

            ViewBag.Event = _eventService.Get(idOfEvent);
            ViewBag.Comments = _commentsService.GetEventComments(idOfEvent);
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        public ActionResult Create(EventDTO eventViewModel)
        {
            int currentUserId = User.Identity.GetUserId<int>();

            _eventService.Create(eventViewModel, currentUserId);
            return RedirectToAction("Index", "UserProfile", new { userId = currentUserId });
        }

        [HttpPost]
        public void AddComment(Comment comment)
        {
            _commentsService.Create(comment);
        }
    }
}