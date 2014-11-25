using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace MyProject.Controllers
{
    public class ProfileController : Controller
    {
        private MyProjectEntities db;

        public ProfileController(MyProjectEntities dataBase)
        {
            this.db = dataBase;
        }

        // GET: /Profile/
        public ActionResult Index()
        {
            var profile = db.Profile.Include(p => p.Country1).Include(p => p.User1);
            return View(profile.ToList());
        }

        // GET: /Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: /Profile/Create
        public ActionResult Create()
        {
            ViewBag.Country = new SelectList(db.Country, "Id", "Name");
            ViewBag.User = new SelectList(db.User, "Id", "Email");
            ViewBag.UserType = new SelectList(db.UserType, "Id", "UserType1");
            return View();
        }

        // POST: /Profile/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profile profile)
        {
            Console.Write(profile);
            User user = profile.User1;
            profile.User = user.Id;

            if (ModelState.IsValid)
            {
                db.Profile.Add(profile);
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Country = new SelectList(db.Country, "Id", "Name", profile.Country);
            ViewBag.User = new SelectList(db.User, "Id", "Email", profile.User);
            ViewBag.UserType = new SelectList(db.UserType, "Id", "UserType1");
            var Profile = db.Profile.Include("User");
            return View(Profile);
        }

        // GET: /Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = new SelectList(db.Country, "Id", "Name", profile.Country);
            ViewBag.User = new SelectList(db.User, "Id", "Email", profile.User);
            return View(profile);
        }

        // POST: /Profile/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,User,PhoneNumber,Description,Country,Picture")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Country = new SelectList(db.Country, "Id", "Name", profile.Country);
            ViewBag.User = new SelectList(db.User, "Id", "Email", profile.User);
            return View(profile);
        }

        // GET: /Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: /Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profile.Find(id);
            db.Profile.Remove(profile);
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
