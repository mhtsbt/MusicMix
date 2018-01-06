using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMix.Data;
using MusicMix.Models;

namespace MusicMix.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDataContext _context;

        public HomeController(AppDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string Reset()
        {
            var votes = _context.Votes.ToList();
            var history = _context.History.ToList();

            _context.RemoveRange(votes);
            _context.RemoveRange(history);

            _context.SaveChanges();

            return "Reset OK!";
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
