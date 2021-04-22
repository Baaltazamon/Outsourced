using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Outsourced.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Outsourced.Implementations.Interfaces;

namespace Outsourced.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectNews _projectNews;
        private readonly ICommunity _community;


        public HomeController(ILogger<HomeController> logger, IProjectNews projectNews, ICommunity community)
        {
            _logger = logger;
            _projectNews = projectNews;
            _community = community;
        }

        public IActionResult Index()
        {

            return View(_projectNews.GetAll());
        }

        public IActionResult Community()
        {
            return PartialView(_community.GetAll());
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
