using KutseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KutseApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Ootan sind oma peole! Palun tule kindlasti";
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Tere hommikust!":"Tere päevast";
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                return View("Thanks", guest); 
            }
            else
            {
                return View();
            }
        }
        public void E_mail(Guest guest)
        {   
            try
            {
                WebMail.SmtpServer = "smpt.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "nikolai.grigorjev01@gmail.com";
                WebMail.Password = "nkeei321";
                WebMail.From = "programmeeriminemvc@gmail.com";
                WebMail.Send("nikolai.grigorjev01@gmail.com", "Vastus kutsele", guest.Name + "vastus" + ((guest.WillAttend ?? false) ?
                    "tuleb peole" : "ei tule peole"));
                ViewBag.Message = "kiri on saatnud!";
            }
            catch(Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kiri";
            }
        }
        GuestContext db = new GuestContext();
        [Authorize] // - сможет увидеть только авторизированный пользователь
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }
    }
}