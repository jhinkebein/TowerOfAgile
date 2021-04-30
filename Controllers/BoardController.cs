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
				TempData["Sharecode"] = b.Sharecode;
				context.Boards.Add(b);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View(b);
			}
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

				var boardItems = context.BoardItems.Where(i => i.BoardId == id).Select(t => t.itemText).ToList();
				ViewData["ItemsList"] = boardItems;
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
				bool uniqueCheck = true;
				var items = context.BoardItems.Where(i => i.BoardId == bId).Select(t => t.itemText).ToList();
				foreach (var item in items)
				{
					if (text == item)
					{
						uniqueCheck = false;
					}
				}
				if (uniqueCheck)
				{
					BoardItem item = new BoardItem();
					item.BoardId = bId;
					item.itemType = type;
					item.itemText = text;

					context.Add(item);
					context.SaveChanges();

					TempData["AddMessage"] = "Item Added!";

					string sc = context.Boards.Where(i => i.BoardId == bId).Select(s => s.Sharecode).SingleOrDefault();
					Board b = new Board();
					b.BoardId = bId;
					b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
					b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
					b.Items = context.BoardItems.Where(i => i.BoardId == bId).ToList();

					var boardItems = context.BoardItems.Where(i => i.BoardId == bId).Select(t => t.itemText).ToList();
					ViewData["ItemsList"] = boardItems;

					return View("GetBoard", b); 
				}
				else
				{
					TempData["AddMessage"] = "New item text must be unique.";
					string sc = context.Boards.Where(i => i.BoardId == bId).Select(s => s.Sharecode).SingleOrDefault();
					Board b = new Board();
					b.BoardId = bId;
					b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
					b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
					b.Items = context.BoardItems.Where(i => i.BoardId == bId).ToList();

					var boardItems = context.BoardItems.Where(i => i.BoardId == bId).Select(t => t.itemText).ToList();
					ViewData["ItemsList"] = boardItems;

					return View("GetBoard", b);
				}
			}
			else
			{
				TempData["AddMessage"] = "New item text cannot be blank.";
				string sc = context.Boards.Where(i => i.BoardId == bId).Select(s => s.Sharecode).SingleOrDefault();
				Board b = new Board();
				b.BoardId = bId;
				b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
				b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
				b.Items = context.BoardItems.Where(i => i.BoardId == bId).ToList();

				var boardItems = context.BoardItems.Where(i => i.BoardId == bId).Select(t => t.itemText).ToList();
				ViewData["ItemsList"] = boardItems;

				return View("GetBoard", b);
			};
		}
		[HttpPost]
		public IActionResult UpdateItem(int id, string type2, string SelectedItem)
		{
		   
			var items = context.BoardItems.Where(i => i.BoardId == id).ToList();

			var typeCheck =
					(from i in items
					where i.itemText == SelectedItem
					select i.itemType).FirstOrDefault();
			var itemId =
					(from i in items
					 where i.itemText == SelectedItem
					 select i.BoardItemId).FirstOrDefault();

			if (typeCheck != type2)
			{

				var item = context.BoardItems.SingleOrDefault(i => i.BoardItemId == itemId);
				item.itemType = type2;
				context.Update(item);
				context.SaveChanges();
				TempData["CategoryMessage"] = "Item category successfully changed!";

				string sc = context.Boards.Where(i => i.BoardId == id).Select(s => s.Sharecode).SingleOrDefault();
				Board b = new Board();
				b.BoardId = id;
				b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
				b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
				b.Items = context.BoardItems.Where(i => i.BoardId == id).ToList();

				var boardItems = context.BoardItems.Where(i => i.BoardId == id).Select(t => t.itemText).ToList();
				ViewData["ItemsList"] = boardItems;

				return View("GetBoard", b);
			}
			else
			{
				TempData["CategoryMessage"] = "Item category cannot be the same.";
				string sc = context.Boards.Where(i => i.BoardId == id).Select(s => s.Sharecode).SingleOrDefault();
				Board b = new Board();
				b.BoardId = id;
				b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
				b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
				b.Items = context.BoardItems.Where(i => i.BoardId == id).ToList();

				var boardItems = context.BoardItems.Where(i => i.BoardId == id).Select(t => t.itemText).ToList();
				ViewData["ItemsList"] = boardItems;

				return View("GetBoard", b);
			} 
			
		}
		[HttpPost]
		public IActionResult DeleteItem(int id, string SelectedItem)
		{
			var items = context.BoardItems.Where(i => i.BoardId == id).ToList();
			var itemId = (from i in items
						  where i.itemText == SelectedItem
						  select i.BoardItemId).FirstOrDefault();

			var item = context.BoardItems.SingleOrDefault(i => i.BoardItemId == itemId);
			context.Remove(item);
			context.SaveChanges();
			TempData["DeleteMessage"] = "Item deleted.";
			string sc = context.Boards.Where(i => i.BoardId == id).Select(s => s.Sharecode).SingleOrDefault();
			Board b = new Board();
			b.BoardId = id;
			b.Name = context.Boards.Where(s => s.Sharecode == sc).Select(n => n.Name).SingleOrDefault();
			b.Sharecode = context.Boards.Where(s => s.Sharecode == sc).Select(s => s.Sharecode).SingleOrDefault();
			b.Items = context.BoardItems.Where(i => i.BoardId == id).ToList();

			var boardItems = context.BoardItems.Where(i => i.BoardId == id).Select(t => t.itemText).ToList();
			ViewData["ItemsList"] = boardItems;

			return View("GetBoard", b);
		}
		[HttpPost]
		public IActionResult DeleteBoard(int id)
		{
			
			var board = context.Boards.Where(i => i.BoardId == id).SingleOrDefault();

			context.RemoveRange(context.BoardItems.Where(i => i.BoardId == id));
			context.Remove(board);
			context.SaveChanges();

			TempData["DeleteBoard"] = $"Board '{board.Name}' has been deleted.";
			return RedirectToAction("Index");
		}
	}
}
	

