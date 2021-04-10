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

                int id = context.Boards.Where(s => s.Sharecode == sc).Select(i => i.BoardId).SingleOrDefault();
                b.BoardId = id;
                b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
                b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
                b.Items = context.BoardItems.Where(i => i.BoardId == id).ToList();
            }
            else
            {
                ViewBag.Total = "Your Sharecode does not exist.";
            }
            return View(b);
        }
        [HttpPost]
        public IActionResult AddItem(int bId, string type, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                BoardItem item = new BoardItem();
                item.BoardId = bId;
                item.itemType = type;
                item.itemText = text;

                context.Add(item);
                context.SaveChanges();

                TempData["message"] = "Item Added!";

                string sc = context.Boards.Where(i => i.BoardId == bId).Select(s => s.Sharecode).SingleOrDefault();
                Board b = new Board();
                b.BoardId = bId;
                b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
                b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
                b.Items = context.BoardItems.Where(i => i.BoardId == bId).ToList();
                return View("GetBoard", b);
            }
            else
            {
                TempData["message"] = "New item text cannot be blank.";
                string sc = context.Boards.Where(i => i.BoardId == bId).Select(s => s.Sharecode).SingleOrDefault();
                Board b = new Board();
                b.BoardId = bId;
                b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
                b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
                b.Items = context.BoardItems.Where(i => i.BoardId == bId).ToList();

                return View("GetBoard", b);
            };

            }
        }
    }
    

