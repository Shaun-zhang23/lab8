using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab8MVC.Models;

namespace Lab8MVC.Controllers
{
    public class UniversityCampusesController : Controller
    {
        private Lab8MvcContext db = new Lab8MvcContext();

        // GET: UniversityCampuses
        public ActionResult Index()
        {
            //return View(db.UniversityCampuses.ToList());
            //LINQ method syntax to order the campuses alphabetically by name
            return View(db.UniversityCampuses.OrderBy(c => c.Name).ToList());
        }

        // GET: UniversityCampuses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityCampus universityCampus = db.UniversityCampuses.Find(id);
            if (universityCampus == null)
            {
                return HttpNotFound();
            }
            return View(universityCampus);
        }

        // GET: UniversityCampuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UniversityCampuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] UniversityCampus universityCampus)
        {
            if (ModelState.IsValid)
            {
                db.UniversityCampuses.Add(universityCampus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(universityCampus);
        }

        // GET: UniversityCampuses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityCampus universityCampus = db.UniversityCampuses.Find(id);
            if (universityCampus == null)
            {
                return HttpNotFound();
            }
            return View(universityCampus);
        }

        // POST: UniversityCampuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] UniversityCampus universityCampus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universityCampus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(universityCampus);
        }

        // GET: UniversityCampuses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityCampus universityCampus = db.UniversityCampuses.Find(id);
            if (universityCampus == null)
            {
                return HttpNotFound();
            }
            return View(universityCampus);
        }

        // POST: UniversityCampuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UniversityCampus universityCampus = db.UniversityCampuses.Find(id);
            db.UniversityCampuses.Remove(universityCampus);
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
