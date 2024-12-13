using ClinicalAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicalAutomation.Controllers
{
    public class SecurityCredenController : Controller
    {
        // GET: SecurityCreden
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            if (ModelState.IsValid)
            {

                Models.clinicalSysdbEntities _db = new clinicalSysdbEntities();

                Models.User usr = _db.Users.SingleOrDefault(dbusr => dbusr.UserName.ToLower() == user
               .UserName.ToLower() && dbusr.Password == user.Password);

                if (usr != null)
                {
                    FormsAuthentication.SetAuthCookie(usr.UserName, false);
                    CurrentUserModel cusr = new CurrentUserModel();
                    cusr.UserName = usr.UserName;
                    cusr.ReferenceToId = usr.ReferenceId;
                    cusr.UserID = usr.UserId;
                    cusr.Role = usr.Role;


                    if (usr.Role == "PHYSICIAN")
                    {

                        cusr.FirstName = _db.physicians.Find(usr.ReferenceId).name;
                        //cusr.LastName = _db.physicians.Find(usr.ReferenceId).LastName;
                    }



                    if (usr.Role.ToUpper() == "ADMIN")
                    {
                        cusr.UserName = _db.Users.Find(usr.UserId).UserName;
                    }

                    if (usr.Role == "PATIENT")
                    {

                        cusr.FirstName = _db.patients.Find(usr.ReferenceId).name;
                        //cusr.LastName = _db.physicians.Find(usr.ReferenceId).LastName;
                    }



                    //if (usr.Role.ToUpper() == "ADMIN")
                    //{
                    //    cusr.FirstName = "ADMIN";
                    //}

                    Session["CurrentUser"] = cusr;
                    return RedirectToAction("Index", usr.Role);
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        //------------------------------------------------------------------------------------------
        // GET: Register


        public clinicalSysdbEntities db = new clinicalSysdbEntities();

        public ActionResult Register()
        {
           return View();
        }

            // POST: Register
            [HttpPost]
            public ActionResult Register(string name, DateTime DOB, int age, string contact, string email, string address, string gender)
            {
                if (ModelState.IsValid)
                {
                    // Set default values for 'status' and 'account'
                    string status = "INACTIVE";
                   

                    // Create a new patient instance
                    var newPatient = new patient
                    {
                        name = name,
                        DOB = DOB,
                        age = age,
                        contact = contact,
                        email = email,
                        address = address,
                        gender = gender,
                        status = status,   // Default 'INACTIVE'
                         // Default 'unregistered'
                    };

                    // Add the new patient to the database
                    db.patients.Add(newPatient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

        
        
    }
}