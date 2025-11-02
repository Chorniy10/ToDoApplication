using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoDem.Models;

namespace ToDoDem.Controllers
{
    public class HomeController : Controller
    {
        public ToDoContext context;
        public HomeController(ToDoContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.categories = context.Categories.ToList();
            ViewBag.statuses = context.Statuses.ToList();
            ViewBag.duefilters = Filters.DueValues;

            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.CategoryId)
                .Include(t => t.StatusId);

            if (filters.hasCategory)
            {
                query = query.Where(t => t.CategoryId == filters.categoryId);
            }
            if (filters.hasStatus)
            {
                query = query.Where(t => t.StatusId == filters.statusId);
            }

            if (filters.hasdue)
            {
                if (filters.isPast)
                {
                    query = query.Where(t => t.DueDate < DateTime.Today);
                }
                else if (filters.isToday)
                {
                    query = query.Where(t => t.DueDate == DateTime.Today);
                }
                else if (filters.isFuture)
                {
                    query = query.Where(t => t.DueDate > DateTime.Today);
                }
            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categories = context.Categories.ToList();
            ViewBag.statuses = context.Statuses.ToList();
            var task = new ToDo { StatusId = "open"};
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.categories = context.Categories.ToList();
                ViewBag.statuses = context.Statuses.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join("-", filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            selected = context.ToDos.Find(selected.Id);
            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new {ID = id});
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var toDelete = context.ToDos.Where(t => t.StatusId == "closed").ToList();
            foreach(var task in toDelete)
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }
    }
}
