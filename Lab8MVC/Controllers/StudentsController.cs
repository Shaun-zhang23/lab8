using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab8MVC.Models;
using Lab8MVC.ViewModels;

namespace Lab8MVC.Controllers
{
    public class StudentsController : Controller
    {
        private Lab8MvcContext db = new Lab8MvcContext();

        // GET: Students
        public ActionResult Index(string campus, string search)
        {
            //instantiate a new view model
            StudentIndexViewModel viewModel = new StudentIndexViewModel();

            var students = db.Students.Include(s => s.UniversityCampus);

            //LINQ syntax to sort the students by campus name, then student name
            students = students.OrderBy(s => s.UniversityCampus.Name).ThenBy(s => s.Name);

            //find the students where either the student name field contains search,
            //the student address contains search, or the student's campus name contains search
            if (!String.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.Name.Contains(search) ||
                                               s.Address.Contains(search) ||
                                               s.UniversityCampus.Name.Contains(search));
                viewModel.Search = search;
            }

            //group search results into campuses and count how many students in each campus
            viewModel.CampusesWithCount = from matchingStudents in students
                                          group matchingStudents by
                                          matchingStudents.UniversityCampus.Name into
                                          campusGroup
                                          select new CampusWithCount()
                                          {
                                              CampusName = campusGroup.Key,
                                              StudentCount = campusGroup.Count()
                                          };

            if (!String.IsNullOrEmpty(campus))
            {
                students = students.Where(s => s.UniversityCampus.Name == campus);
            }

            //ViewBag version of the campus filter (tutorial step "Filtering the search
            //results by campus using View Bag"), replaced by the View Model below:
            //ViewBag.Campus = new SelectList(
            //    students.OrderBy(s => s.UniversityCampus.Name)
            //            .Select(s => s.UniversityCampus.Name).Distinct());

            viewModel.Students = students;
            viewModel.Campus = campus;

            return View(viewModel);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.UniversityCampuses, "ID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.UniversityCampuses, "ID", "Name", student.CampusID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.UniversityCampuses, "ID", "Name", student.CampusID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.UniversityCampuses, "ID", "Name", student.CampusID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
