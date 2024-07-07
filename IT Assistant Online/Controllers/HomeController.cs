using IT_Assistant_Online.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace IT_Assistant_Online.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var posts = db.Posts.OrderByDescending(x=>x.Date).Take(3);
            return View(posts);
        }

        // Get
        [Authorize]
        public ActionResult Location()
        {
            ViewBag.Section = new SelectList(db.Sections, "Name", "Name");
            return View();
        }

        // Post
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        
        public ActionResult Location(Location location, double lOng, double lat, string message, string section)
        {
            if(ModelState.IsValid)
            {
                location.UserID = User.Identity.GetUserId();
                location.Long = lOng;
                location.Lat = lat;
                location.Message = message;
                location.Section = section;

                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index", "Manage", new { Message = ManageController.ManageMessageId.LocateSuccess });

            }

            else
            {
                ViewBag.Section = new SelectList(db.Sections, "Name", "Name");
                return View();

            }

            
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search( string search)
        {
            var result = db.Posts.Where(a => a.Title.Contains(search)
            || a.Contenet.Contains(search)).ToList();

            return View(result);
        }

    }
}