using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmiOfficial.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index() 
        {
            //ViewBag.Event = _eventService.GetEvent(eventId);
           // ViewBag.Comments = _eventService.GetComments(eventId);
            //
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
            c2.AuthorId = 2;
            c2.AuthorName = "Author 2";
            c2.Id = 101;
            c2.ImageName = "https://static.pexels.com/photos/6550/nature-sky-sunset-man.jpeg";
            c2.WritingDate = DateTime.Now;
            c2.Text = "Very nice comment 2";
            List<Comment> c = new List<Comment>();
            c.Add(c1);
            c.Add(c2);
            ViewBag.Comments = c;
            return View();
        }

        [HttpPost]
        public void AddComment(int EventId,int UserId,string Text)
        {
            int a = 0;
            return;
            //Index(EventId, UserId);
        }
        [HttpPost]
        public void AddComment( string Text)
        {
            int a = 0;
            return;
        }
    }
}