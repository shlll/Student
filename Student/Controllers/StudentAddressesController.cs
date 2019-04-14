using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Student.Models;

namespace Student.Controllers
{
    public class StudentAddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentAddresses
        public ActionResult Index(int? page, string searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;
            int pageSize = 2; // display three blog posts at a time on this page
            int pageNumber = (page ?? 1);
            var postQuery = db.Address.OrderBy(p => p.Address1).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                postQuery = postQuery.Where(p => p.Address1.Contains(searchQuery)
                || p.Address2.Contains(searchQuery)
                || p.City.Contains(searchQuery)
                || p.Country.Contains(searchQuery)
                || p.State.Contains(searchQuery));
            }
          
            var studentAddresses = postQuery.ToPagedList(pageNumber ,pageSize);
            return View(studentAddresses);
        }

        // GET: StudentAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAddress studentAddress = db.Address.Find(id);
            if (studentAddress == null)
            {
                return HttpNotFound();
            }
            return View(studentAddress);
        }

        // GET: StudentAddresses/Create
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: StudentAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address1,Address2,City,Zipcode,State,Country,StudentId,UserId")] StudentAddress studentAddress)
        {
            if (ModelState.IsValid)
            {
                db.Address.Add(studentAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentName", studentAddress.StudentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", studentAddress.UserId);
            return View(studentAddress);
        }

        // GET: StudentAddresses/Edit/5
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAddress studentAddress = db.Address.Find(id);
            if (studentAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentName", studentAddress.StudentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", studentAddress.UserId);
            return View(studentAddress);
        }

        // POST: StudentAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address1,Address2,City,Zipcode,State,Country,StudentId,UserId")] StudentAddress studentAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentName", studentAddress.StudentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", studentAddress.UserId);
            return View(studentAddress);
        }

        // GET: StudentAddresses/Delete/5
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAddress studentAddress = db.Address.Find(id);
            if (studentAddress == null)
            {
                return HttpNotFound();
            }
            return View(studentAddress);
        }

        // POST: StudentAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAddress studentAddress = db.Address.Find(id);
            db.Address.Remove(studentAddress);
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
