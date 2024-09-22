using ASP.Net_MVC.DataBases;
using ASP.Net_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.Net_MVC.Controllers
{
    public class WorkerController : Controller
    {
        private readonly MainDbContext db;

        public WorkerController()
        {
            db = new MainDbContext();
        }

        public ActionResult Index(string? searchline, string sort = "FirstName", string sortDir = "Ascend")
        {
            List<Worker> workers = new List<Worker>();
            db.Events.Load();
            switch (sort)
            {
                case "FirstName":
                    if (sortDir == "Ascend")
                    {
                        workers = db.Workers.OrderBy(a => a.FirstName).ToList();
                    }
                    else
                    {
                        workers = db.Workers.OrderByDescending(a => a.FirstName).ToList();
                    }
                    break;
                case "LastName":
                    if (sortDir == "Ascend")
                    {
                        workers = db.Workers.OrderBy(a => a.LastName).ToList();
                    }
                    else
                    {
                        workers = db.Workers.OrderByDescending(a => a.LastName).ToList();
                    }
                    break;
                case "Position":
                    if (sortDir == "Ascend")
                    {
                        workers = db.Workers.OrderBy(a => a.Position).ToList();
                    }
                    else
                    {
                        workers = db.Workers.OrderByDescending(a => a.Position).ToList();
                    }
                    break;
            }
            if (searchline != null)
            {
                workers = workers.Where(a => a.FirstName.Contains(searchline)).ToList();
            }
            return View(workers);
        }



        public ActionResult Details(int id)
        {
            var wk = db.Workers.Single(wk => wk.Id == id);
            db.Events.Where(ev => ev.WorkerId == wk.Id).Load();

            if(wk == null)
            {
                return NotFound();
            }

            return View(wk);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string firstName, string lastName, string position)
        {
            try
            {
                db.Add(new Worker(firstName, lastName, position));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex;
                return RedirectToPage("ErrorPage");
            }

            return Redirect("Index");
        }



        public ActionResult Edit(int id)
        {
            Worker wk = db.Workers.Single(a => a.Id == id);
            if (wk == null)
            {
                return NotFound();
            }

            return View(wk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string firstName, string lastName, string position)
        {
            try
            {
                db.Workers.Update(new Worker(id, firstName, lastName, position));
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
            Worker wk = db.Workers.Single(a => a.Id == id);
            if (wk == null)
            {
                return NotFound();
            }

            db.Workers.Remove(wk);
            db.SaveChanges();
            return Redirect("Index");
        }
    }
}
