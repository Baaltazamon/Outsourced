using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Outsourced.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Outsourced.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        OutsourcedContext db = new OutsourcedContext();
        
        public IActionResult Index()
        {
            
            return View(Tuple.Create(db.ProjectNews.ToList(), db.Communities.Where(c=> c.DateWebinar > DateTime.Now).ToList(),
                db.Requests.ToList(), db.Users.ToList(), db.TypeRequests.ToList()));
        }

        public IActionResult AddRequest(string name, string email, string message)
        {
            var us = db.Users.SingleOrDefault(c => c.Email == email);
            string[] fio = name.Split(" ");
            if (us is null)
            {
                us = new User
                {
                    Email = email,
                    LastName = fio[0],
                    FirstName = (fio.Length > 1) ? fio[1] : " ",
                    MiddleName = (fio[2].Length > 2) ? fio[2] : " "
                };
                db.Users.Add(us);
                db.SaveChanges();
            }
            var req = new Request
            {
                UserRequest = email,
                Wishes = message
            };
            db.Requests.Add(req);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddFullRequest(string name, string email, string category, string message,  string file_attach)
        {
            var us = db.Users.SingleOrDefault(c => c.Email == email);
            string[] fio = name.Split(" ");
            if (us is null)
            {
                us = new User
                {
                    Email = email,
                    LastName = fio[0],
                    FirstName = (fio.Length > 1) ? fio[1] : " ",
                    MiddleName = (fio.Length > 2) ? fio[2] : " "
                };
                db.Users.Add(us);
                db.SaveChanges();
            }

            var type = db.TypeRequests.SingleOrDefault(c => c.NameTypeRequest == category.Trim());
            if (type is null)
            {
                type = new TypeRequest
                {
                    NameTypeRequest = category.Trim()
                };
                db.TypeRequests.Add(type);
                db.SaveChanges();
            }
            var req = new Request
            {
                UserRequest = email,
                Wishes = message,
                TechnicalTask = file_attach,
                Type = category.Trim()
            };
            db.Requests.Add(req);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
