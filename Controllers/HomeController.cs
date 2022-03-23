using KutseApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            string pidu = "";
            if (DateTime.Now.Month == 1) { pidu = "Jõulud"; }
            else if (DateTime.Now.Month == 2) { pidu = "Heast puhkus pidu"; }
            else if (DateTime.Now.Month == 3) { pidu = "Naistepäev"; }
            else if (DateTime.Now.Month == 4) { pidu = "Aprillinali"; }
            else if (DateTime.Now.Month == 5) { pidu = "Võidupüha pidu"; }
            else if (DateTime.Now.Month == 6) { pidu = "Lastekaitsepäev"; }
            else if (DateTime.Now.Month == 7) { pidu = "Spordiajakirjaniku päev"; }
            else if (DateTime.Now.Month == 8) { pidu = "Nostalgiline päev"; }
            else if (DateTime.Now.Month == 9) { pidu = "Teadmiste päev"; }
            else if (DateTime.Now.Month == 10) { pidu = "Ülemaailmne loomade päev"; }
            else if (DateTime.Now.Month == 11) { pidu = "Ennustamispäev kohvipaksu peal"; }
            else if (DateTime.Now.Month == 12) { pidu = "Vanaaasta õhtu"; }

            ViewBag.Message = "Ootan sind oma peole! " + pidu + " Palun tule kindlasti!";

            int hour = DateTime.Now.Hour;
            if (hour <= 16)
            {
                ViewBag.Greeting = hour < 10 ? "Tere hommikust" : "Tere päevast";
            }
            else if (hour > 16)
            {
                ViewBag.Greeting = hour < 20 ? "Tere õhtu" : "Tere päevast";
            }
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        [HttpPost]
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [Authorize]
        public ActionResult GuestsJah()
        {
            IEnumerable<Guest> guest = db.Guests.Where(g => g.WillAttend == true);
            return View(guest);
        }
        public ActionResult GuestsEi()
        {
            IEnumerable<Guest> guest = db.Guests.Where(g => g.WillAttend == false);
            return View(guest);
        }
        PiduContext pd = new PiduContext();
        [Authorize]
        public ActionResult Pidus()
        {
            IEnumerable<Pidu> pidus = pd.Pidus;
            return View(pidus);
        }
        public ActionResult Createp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Createp(Pidu pidu)
        {
            pd.Pidus.Add(pidu);
            pd.SaveChanges();
            return RedirectToAction("Pidus");
        }

        public ActionResult Deletep(int id)
        {
            Pidu p = pd.Pidus.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost, ActionName("Deletep")]
        public ActionResult DeleteConfirmedp(int id)
        {
            Pidu p = pd.Pidus.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            pd.Pidus.Remove(p);
            pd.SaveChanges();
            return RedirectToAction("Pidus");
        }

        [HttpGet]
        public ActionResult Editp(int? id)
        {
            Pidu p = pd.Pidus.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost, ActionName("Editp")]
        public ActionResult EditConfirmedp(Pidu p)
        {
            pd.Entry(p).State = System.Data.Entity.EntityState.Modified;
            pd.SaveChanges();
            return RedirectToAction("Pidus");
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
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kiri";
            }
        }
        public void Thanks(Guest guest)
        {
            WebMail.Send(guest.Email, "Meeldetuletus ", guest.Name + " ära unusta. Sind ootavad " + ((guest.WillAttend ?? false) ? " tuleb peole: " : " ei tule peole "));
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