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

        public ActionResult Index(int? userId, int? eventId) 
        {
            //uncomment when CommentsDao is implemented

            //ViewBag.Event = _eventService.Get(eventId);
            //ViewBag.Comments = _commentsService.GetEventComments(eventId);
            //ViewBag.UserId = userId;

            Event dummyEvent = new Event();

            dummyEvent.Decription = "This is event example";
            dummyEvent.Id = 0;
            dummyEvent.ImageId = 1001;
            dummyEvent.Location = "Lwiw";
            dummyEvent.Name = "event example";
            dummyEvent.OrganizerId = 1;
            dummyEvent.Time = DateTime.Now;

            ViewBag.Event = dummyEvent;
            Comment c1 = new Comment();
            c1.AuthorId = 1;
            c1.AuthorName = "Author 1";
            c1.Id = 100;
            c1.ImageName = "https://static.pexels.com/photos/6550/nature-sky-sunset-man.jpeg";
            c1.WritingDate = DateTime.Now;
            c1.Text = "Very nice comment 1";
            Comment c2 = new Comment();
            c1.AuthorId = 2;
            c1.AuthorName = "Author 2";
            c1.Id = 101;
            c1.ImageName = "https://static.pexels.com/photos/6550/nature-sky-sunset-man.jpeg";
            c1.WritingDate = DateTime.Now;
            c1.Text = "Very nice comment 2";
            List<Comment> c = new List<Comment>();
            c.Add(c1);
            c.Add(c2);
            ViewBag.Comments = c;
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventDTO eventViewModel)
        {
            int currentUserId = User.Identity.GetUserId<int>();

            _eventService.Create(eventViewModel, currentUserId);
            return RedirectToAction("Index", "UserProfile", new { userId = currentUserId });
        }

        public void AddComment(Comment comment)
        {
            _commentsService.Create(comment);
        }
    }
}