using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IT_Assistant_Online.Models;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Data.Entity;

namespace IT_Assistant_Online.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db;
        public ManageController()
        {
            db = new ApplicationDbContext();
        }


        // Get
        public ActionResult UpdatProfilePicture()
        {           
            return View();
        }

        //Post
        [HttpPost]
        public ActionResult UpdatProfilePicture(HttpPostedFileBase upload)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/images/profile"),upload.FileName);
                upload.SaveAs(path);
                user.UserImage = upload.FileName;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ShowLocationMessages()
        {
            var message = db.Locations.Where(x=>!x.IsRead);
            return View(message.ToList());            
        }

        public ActionResult MarkReadMessage(int id)
        {
            Location location = db.Locations.Find(id);
            location.Lat = location.Lat;
            location.Long = location.Long;
            location.Message = location.Message;
            location.Section = location.Section;
            location.UserID = location.UserID;

            location.IsRead = true;

            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowLocationMessages", "Manage");
            
        }

        [Authorize]
        // GET: Details
        public ActionResult LocationDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Locations.Find(id);

            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "تم تعديل كلمة المرور بنجاح"
                : message == ManageMessageId.Error ? "حصل خطأ معين!."
                : message == ManageMessageId.LocateSuccess ? "تم تحديد مكانك بنجاح"
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        // Get
        public ActionResult Upgrade(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");
            return View(user);
        }

        // Post
        [HttpPost]
        public ActionResult Upgrade(string Role, ApplicationUser user)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            UserManager.RemoveFromRole(user.Id, "Users");
            UserManager.AddToRole(user.Id,Role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get
        public ActionResult AddPhoneNumber()
        {
            return View();
        }


        // Post
        [HttpPost]
        public ActionResult AddPhoneNumber( string number)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());

            if (number != null)
            {
                user.PhoneNumber = number;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error,
            LocateSuccess
        }

        #endregion
    }
}