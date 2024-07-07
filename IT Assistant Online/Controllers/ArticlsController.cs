using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Assistant_Online.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace IT_Assistant_Online.Controllers
{
    public class ArticlsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articls
        public ActionResult Index()
        {
            
            var articls = db.Articls.Include(a => a.User);
            return View(articls.ToList());
        }

        // GET: Articls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articl articl = db.Articls.Find(id);
            if (articl == null)
            {
                return HttpNotFound();
            }
            return View(articl);
        }
        
        [Authorize(Roles = "Admin,ComputerExpert,ElectronicExpert,MobileExpert")]
        // GET: Articls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articls/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ComputerExpert,ElectronicExpert,MobileExpert")]
        public ActionResult Create( Articl articl, HttpPostedFileBase upload)
        {
                string path = Path.Combine(Server.MapPath("~/image_upload/Articles"), upload.FileName);
                upload.SaveAs(path);
                articl.ArticleImage = upload.FileName;

                articl.UserID = User.Identity.GetUserId();
                articl.Date = DateTime.Now;
                db.Articls.Add(articl);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: Articls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articl articl = db.Articls.Find(id);
            if (articl == null)
            {
                return HttpNotFound();
            }
            return View(articl);
        }

        // POST: Articls/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Articl articl , HttpPostedFileBase upload)
        {
              string path = Path.Combine(Server.MapPath("~/image_upload/Articles"), upload.FileName);
              upload.SaveAs(path);
              articl.ArticleImage = upload.FileName;

               articl.UserID = User.Identity.GetUserId();
               articl.Date = DateTime.Now;

               db.Entry(articl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: Articls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articl articl = db.Articls.Find(id);
            if (articl == null)
            {
                return HttpNotFound();
            }
            return View(articl);
        }

        // POST: Articls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articl articl = db.Articls.Find(id);
            db.Articls.Remove(articl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
