using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Assistant_Online.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using PagedList;

namespace IT_Assistant_Online.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(int? page)
        {
            var posts = db.Posts.Include(p => p.User).OrderByDescending(x => x.Date);
            return View(posts.ToList().ToPagedList(page ?? 1 ,5));
        }

        [Authorize]
        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [Authorize]
        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post post, string categories, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/image_upload"), upload.FileName);
                upload.SaveAs(path);
                post.Image = upload.FileName;
            }

            post.Date = DateTime.Now;
            post.UserID = User.Identity.GetUserId();

            post.Categories.Clear();
            post.Categories.Add(GetCategory(categories));

            if (post.Contenet == null || post.Title == null)
            {
                ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
                return View(post);
            }
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = post.ID });
        }


        [Authorize]
        [ValidateInput(false)]
        public ActionResult Comment(int id, string contenet)
        {
                Post post = GetPost(id);
                Comment comment = new Comment();
                comment.Post = post;
                comment.Date = DateTime.Now;
                comment.UserID = User.Identity.GetUserId();
                comment.Contenet = contenet;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });   
        }

        public ActionResult MarkSolvedPosts(int id)
        {
            Post post = GetPost(id);
            post.Categories = post.Categories;
            post.Comments = post.Comments;
            post.Contenet = post.Contenet;
            post.Date = post.Date;
            post.ID = post.ID;
            post.Image = post.Image;
            post.SectionId = post.SectionId;
            post.UserID = post.UserID;

            post.IsSolved = true;

            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Message = "تم حل المشكلة";
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult UnSolvedPosts()
        {
            var posts = db.Posts.Include(p => p.User).Where(x => !x.IsSolved);
            return View(posts.ToList());
            
        }
        private Post GetPost(int? id)
        {
            return id.HasValue ? db.Posts.Where(x => x.ID == id).First() : new Post() { ID = -1 };
        }

        [Authorize]
        public ActionResult Categories(string id)
        {      
            Category cate = GetCategory(id);
            return View("Index", cate.Posts.ToPagedList(1,5));
        }

        private Category GetCategory(string CateName)
        {
            return db.Categories.Where(x => x.Name == CateName).FirstOrDefault() ?? new Category() { Name = CateName };
        }

        public ActionResult Sections(int id)
        {
            if(id == 1)
            {
                ViewBag.Image = "1";
            }
            else if (id == 2)
            {
                ViewBag.Image = "2";
            }
            else if (id == 3)
            {
                ViewBag.Image = "3";
            }
            return View(db.Posts.Where(X => X.SectionId == id));
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", post.UserID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
           
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, string categories, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/image_upload"), upload.FileName);
                upload.SaveAs(path);
                post.Image = upload.FileName;
            }

            post.Categories.Clear();
            post.Categories.Add(GetCategory(categories));

            post.Date = DateTime.Now;
            post.UserID = User.Identity.GetUserId();
           
           db.Entry(post).State = EntityState.Modified;
           db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
