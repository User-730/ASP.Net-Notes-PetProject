using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP.Net_MVC.Models;
using ASP.Net_MVC.DataBases;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ASP.Net_MVC.Controllers
{
    public class EventListController : Controller
    {
        private readonly MainDbContext db;

        public EventListController()
        {

            db = new MainDbContext();
        }

        public ActionResult Index(string? searchline, string sort = "Title", string sortDir = "Ascend")
        {
            List<EventAction> evs = new List<EventAction>();
            db.Workers.Load();

            switch (sort)
            {
                case "Title":
                    if (sortDir == "Ascend")
                    {
                        evs = db.Events.OrderBy(a => a.Title).ToList();
                    }
                    else
                    {
                        evs = db.Events.OrderByDescending(a => a.Title).ToList();
                    }
                    break;
                case "EventDate":
                    if (sortDir == "Ascend")
                    {
                        evs = db.Events.OrderBy(a => a.EventDate).ToList();
                    }
                    else
                    {
                        evs = db.Events.OrderByDescending(a => a.EventDate).ToList();
                    }
                    break;
            }
            if (searchline != null)
            {
                evs = evs.Where(a => a.Title.Contains(searchline)).ToList();
            }
            return View(evs);
        }



        public ActionResult Details(int id)
        {
            var ev = db.Events.Single(ev => ev.Id == id);
            ev.Worker = db.Workers.Single(wk => wk.Id == ev.WorkerId);

            if(ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }



        public ActionResult Create()
        {
            return View(db.Workers.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventAction act)
        {
            try
            {
                db.Add(act);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex;
                return RedirectToPage("ErrorPage");
            }

            return Redirect("Index");
        }

         

        public ActionResult Edit(int id)
        {
            var ev = db.Events.Single(ev => ev.Id == id);
            db.Workers.Where(wk => wk.Id == ev.WorkerId).Load();

            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string Title, string Type, DateTime EventDate, int WorkerId)
        {
            EventAction act = new EventAction(Id, Type, EventDate, WorkerId, Title);

            try
            {
                db.Events.Update(act);
                db.SaveChanges();
            }
            catch (Exception ex) 
            {
                ViewBag.ErrorMessage = ex;
                return RedirectToPage("ErrorPage");
            }
            return RedirectToAction("Index");
        }



        public ActionResult Delete(int id)
        {
            var ev = db.Events.Single(a => a.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            db.Events.Remove(ev);
            db.SaveChanges();
            return Redirect("Index");
        }
    }
}
