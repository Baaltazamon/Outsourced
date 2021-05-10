using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Outsourced.Models;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Outsourced.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        
        OutsourcedContext db = new OutsourcedContext();
        // GET: AdminController/Details/5
        public bool CheckUser()
        {
            var check = UserIdentity.username.SingleOrDefault(c => Equals(c.ip, UserIdentity.GetIPAddress()));
            if (check == null)
                return true;
            else
            {
                return false;
            }
        }
        public ActionResult Authorization(string login, string password)
        {
            
            var Admin = db.Administrators.SingleOrDefault(c => c.UserName == login && c.Password == password);
            if (Admin is null)
                return NotFound();
            var us = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()));
            if (us is null)
            {
                UserIdentity.username.Add(new LogEnter
                {
                    username = Admin.UserName,
                    dateEnter = DateTime.Now,
                    ip = UserIdentity.GetIPAddress()
                });
            }
            else
            {
                us.username = Admin.UserName;
                us.dateEnter = DateTime.Now;
            }

            
            return RedirectToAction(nameof(AdminPanel));
        }

        // GET: AdminController/Create
        public ActionResult AdminPanel()
        {
            var us = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()));
            if (us is null)
                RedirectToAction(nameof(Index));
            else
            {
                if (us.dateEnter.AddMinutes(30) < DateTime.Now)
                    UserIdentity.username.Remove(us);
            }
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            return View(Tuple.Create(db.ProjectNews.ToList(), db.Communities.ToList(),
                db.Requests.Where(c=>c.Administrator==null).ToList(), db.Users.ToList(), db.TypeRequests.ToList(), db.Administrators.ToList()));
        }

        // POST: AdminController/Create
        
        // GET: AdminController/Edit/5
        [HttpGet]
        public ActionResult Edit(string us)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var adm = db.Administrators.SingleOrDefault(c => c.UserName.Equals(us));
            if (adm is null)
                return NotFound();

            return View(adm);
        }

        public ActionResult NewAdmin()
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult AddNew(string username, string lastname, string firstname, string middlename, string email)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            Sendler s = new Sendler();
            var adm = db.Administrators.SingleOrDefault(c => c.UserName == username);
            if (adm is null)
            {
                string pas = s.GeneratePassword();
                adm = new Administrator
                {
                    UserName = username,
                    LastName = lastname,
                    FirstName = firstname,
                    MiddleName = middlename,
                    Password = pas
                };
                db.Administrators.Add(adm);
                ViewBag.Message = s.Send(email, $"Логин: {adm.UserName}\nПароль: {pas}", "Ваши данные для панели администратора");
            }
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        public ActionResult NewWebinar()
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            return View(db.TypeRequests.ToList());
        }
        public IActionResult AddNewWebinar(string header, string type, string description, DateTime datewebinar)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var ad = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()));
            if (ad != null)
            {
                var web = new Community
                {
                    Header = header,
                    Type = type,
                    Administrator = ad.username,
                    DateWebinar = datewebinar,
                    Description = description
                };
                db.Communities.Add(web);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }

        public ActionResult NewProjectNews()
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult AddNewProjectNews(string header, string subheader, string description, IFormFile background, DateTime daterelease)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var ad = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()));
            var dir = Environment.CurrentDirectory + "\\wwwroot\\img";
            string newPath = header + background.FileName;
            using (var fileStream = new FileStream(Path.Combine(dir, newPath), FileMode.Create, FileAccess.Write))
            {
                background.CopyTo(fileStream);
            }
            if (ad != null)
            {
                ProjectNews pn = new ProjectNews
                {
                    Header = header,
                    SubHeader = subheader,
                    DateRelease = daterelease,
                    Description = description,
                    BackGround = $"img/{newPath}",
                    Administrator = ad.username
                };
                db.ProjectNews.Add(pn);
            }

            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        public IActionResult EditAdmin(string username, string lastname, string firstname, string middlename)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var adm = db.Administrators.SingleOrDefault(c => c.UserName == username);
            if (adm is null)
                return NotFound();
            else
            {
                adm.UserName = username;
                adm.LastName = lastname;
                adm.FirstName = firstname;
                adm.MiddleName = middlename;
            }

            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }

        public ActionResult EditWebinar(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var pn = db.Communities.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();

            return View(Tuple.Create(pn, db.TypeRequests.ToList()));
        }
        public IActionResult EditWebinarConfirm(int id, string header, string type, string description, DateTime datewebinar)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var ad = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()));
            var pn = db.Communities.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();
            else
            {
                pn.Header = header;
                pn.Type = type;
                pn.DateWebinar = datewebinar;
                pn.Description = description;
                if (ad != null) pn.Administrator = ad.username;
            }

            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        public ActionResult EditProjectNews(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var pn = db.ProjectNews.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();

            return View(pn);
        }
        public IActionResult EditProjectNewsConfirm(int id, string header, string subheader, string description, IFormFile background, DateTime daterelease)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var ad = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()));
            var pn = db.ProjectNews.SingleOrDefault(c => c.Id == id);
            string newPath = "";
            if (background != null)
            {
                var dir = Environment.CurrentDirectory + "\\wwwroot\\img";
                newPath = ad.username + background.FileName;
                using (var fileStream = new FileStream(Path.Combine(dir, newPath), FileMode.Create, FileAccess.Write))
                {
                    background.CopyTo(fileStream);
                }
            }
            
            if (pn is null)
                return NotFound();
            else
            {
                pn.Header = header;
                pn.SubHeader = subheader;
                pn.DateRelease = daterelease;
                pn.Description = description;
                
                if (ad != null) pn.Administrator = ad.username;
            }

            if (background != null)
            {
                pn.BackGround = $"img/{newPath}";
            }
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        // POST: AdminController/Edit/5

        public ActionResult Delete(string username)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var adm = db.Administrators.SingleOrDefault(c => c.UserName == username);
            if (adm is null)
                return NotFound();
            return View(adm);
        }

        public ActionResult ConfirmRequest(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var rq = db.Requests.SingleOrDefault(c => c.Id == id);
            if (rq == null)
                NotFound();
            if (rq != null)
                rq.Administrator = UserIdentity.username.SingleOrDefault(c => c.ip.Equals(UserIdentity.GetIPAddress()))
                    ?.username;
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        public ActionResult DeleteProjectNews(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var pn = db.ProjectNews.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();
            return View(pn);
        }
        public ActionResult DeleteWebinar(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var pn = db.Communities.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();
            return View(pn);
        }
        public ActionResult DeleteWebinarConfirm(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var pn = db.Communities.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();
            db.Communities.Remove(pn);
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        public ActionResult DeletePrijectNewsConfirm(int id)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var pn = db.ProjectNews.SingleOrDefault(c => c.Id == id);
            if (pn is null)
                return NotFound();
            db.ProjectNews.Remove(pn);
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        public ActionResult DeleteAdmin(string username)
        {
            if (CheckUser())
                return RedirectToAction(nameof(Index));
            var adm = db.Administrators.SingleOrDefault(c => c.UserName == username);
            if (adm is null)
                return NotFound();
            db.Administrators.Remove(adm);
            db.SaveChanges();
            return RedirectToAction(nameof(AdminPanel));
        }
        // POST: AdminController/Delete/5
        
        
    }
}
