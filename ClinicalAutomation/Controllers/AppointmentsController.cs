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
    public class AppointmentsController : Controller
    {
        private clinicalSysdbEntities db = new clinicalSysdbEntities();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments
        .Include(a => a.patient)
        .Include(a => a.physician)
        .ToList(); // Fetch all appointments
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public ActionResult AppointmentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult AppointmentCreate()
        {
            ViewBag.PatientID = new SelectList(db.patients, "patientID", "name");
            ViewBag.PhysicianID = new SelectList(db.physicians, "physicianID", "name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentCreate([Bind(Include = "AppointmentID,PatientID,PhysicianID,AppointmentDate,Criticality,Reason,Note,Status,ScheduleDateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Status = "Pending";
                
                // Default status for new requests
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientID = new SelectList(db.patients, "patientID", "name", appointment.PatientID);
            ViewBag.PhysicianID = new SelectList(db.physicians, "physicianID", "name", appointment.PhysicianID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult AppointmentEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.patients, "patientID", "name", appointment.PatientID);
            ViewBag.PhysicianID = new SelectList(db.physicians, "physicianID", "name", appointment.PhysicianID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentEdit([Bind(Include = "AppointmentID,PatientID,PhysicianID,AppointmentDate,Criticality,Reason,Note,Status,ScheduleDateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.patients, "patientID", "name", appointment.PatientID);
            ViewBag.PhysicianID = new SelectList(db.physicians, "physicianID", "name", appointment.PhysicianID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult AppointmentDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("AppointmentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //---------------------------------------------------------------------

        // GET: Appointments/Confirm/5
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            ViewBag.PhysicianID = new SelectList(db.physicians, "physicianID", "name");
            return View(appointment);
        }

        // POST: Appointments/Confirm/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(int id, int physicianId, DateTime scheduleDateTime)
        {
            var appointment = db.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.PhysicianID = physicianId;
                appointment.ScheduleDateTime = scheduleDateTime;
                appointment.Status = "Confirmed"; // Update the status

                db.SaveChanges();
            }

            return RedirectToAction("ViewPendingAppointments");
        }




        // In case of validation fail
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


