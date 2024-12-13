using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicalAutomation.Models;

namespace ClinicalAutomation.Controllers
{
    public class chemistsController : Controller
    {
        private clinicalSysdbEntities db = new clinicalSysdbEntities();

        // GET: chemists
        public ActionResult Index()
        {
            return View(db.chemists.ToList());
        }

        // GET: chemists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chemist chemist = db.chemists.Find(id);
            if (chemist == null)
            {
                return HttpNotFound();
            }
            return View(chemist);
        }

        // GET: chemists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: chemists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chemistID,name,contact,email")] chemist chemist)
        {
            if (ModelState.IsValid)
            {
                db.chemists.Add(chemist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chemist);
        }

        // GET: chemists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chemist chemist = db.chemists.Find(id);
            if (chemist == null)
            {
                return HttpNotFound();
            }
            return View(chemist);
        }

        // POST: chemists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chemistID,name,contact,email")] chemist chemist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chemist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chemist);
        }

        // GET: chemists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chemist chemist = db.chemists.Find(id);
            if (chemist == null)
            {
                return HttpNotFound();
            }
            return View(chemist);
        }

        // POST: chemists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chemist chemist = db.chemists.Find(id);
            db.chemists.Remove(chemist);
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
