using ClinicalAutomation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicalAutomation.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }


        // GET: Appointments/Create
        private clinicalSysdbEntities db = new clinicalSysdbEntities();



        [HttpGet]
        public ActionResult RequestAppointment()
        {
            // Render the form
            return View(new Appointment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Assuming PatientID is stored in the session or cookie
                //appointment.PatientID = (int)Session["PatientID"]; // Or retrieve it from authenticated user
                appointment.Status = "PENDING"; // Default status

                var usr = Session["CurrentUser"] as Models.CurrentUserModel;

              var pas=  db.patients.Find(usr.ReferenceToId);
                appointment.patient = pas;
                db.Appointments.Add(appointment); // Add to database
                db.SaveChanges(); // Save changes
                TempData["SuccessMessage"] = "Your appointment request has been sent.";
                return RedirectToAction("Index", "Patient"); // Redirect to patient home
            }

            return View(appointment); // Reload the form if invalid
        }







        //public ActionResult appointIndex()
        //{
        //    ViewBag.PatientID = new SelectList(db.patients, "patientID", "name");
        //    ViewBag.PhysicianID = new SelectList(db.physicians, "physicianID", "name");
        //    return View();
        //}


    }
}