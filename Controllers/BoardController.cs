using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerOfAgile.Models;

namespace TowerOfAgile.Controllers
{
    public class BoardController : Controller
    {
        private BoardDbContext context { get; set; }
        public BoardController(BoardDbContext ctx) => context = ctx;
        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");
        [HttpGet]
        public ActionResult Create() //Called when blank
        {
            ViewBag.Total = "";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Board b)
        {

            if (ModelState.IsValid)
            {
                b.Sharecode = b.createShareCode();
                ViewBag.Total = b.Sharecode;
            }
            else
            {
                ViewBag.Total = "";
            }
            context.Boards.Add(b);
            context.SaveChanges();
            return View(b);

        }
        [HttpGet]
        public ActionResult GetBoard()
        {
            Board b = new Board();
            ViewBag.Total = "";
            return View(b);
        }
        [HttpPost]
        public IActionResult GetBoard(string sc)
        {
            Board b = new Board();
            if (context.Boards.Any(s => s.Sharecode == sc))
            {
                b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
                b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
            }
            else
            {
                ViewBag.Total = "Your Sharecode does not exist.";
            }
            return View(b);
        }
        
    }
}
