using ASP.Net_MVC.DataBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP.Net_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace ASP.Net_MVC.Controllers
{
    public class CalendarController : Controller
    {
        private readonly MainDbContext db;

        public CalendarController()
        {
            db = new MainDbContext();
        }
        public ActionResult Index(int year = 0, int month = 0)
        {
            List<DateTime> dates = Calendar.GetDates(year, month);
            ViewBag.Marks = db.Marks.Where(m => (m.DateTime >= dates[0] && m.DateTime <= dates[dates.Count-1])).ToList();

            return View(dates);
        }


        public ActionResult Details(int year, int month, int day, 
            string? searchline, string sort = "Title", string sortDir = "Ascend")
        {
            List<Mark> marks = new List<Mark>();
            DateTime date = new DateTime(year, month, day);

            switch (sort)
            {
                case "Title":
                    if (sortDir == "Ascend")
                    {
                        marks = db.Marks.Where(m => m.DateTime.Date == date.Date).OrderBy(m => m.Title).ToList();
                    }
                    else
                    {
                        marks = db.Marks.Where(m => m.DateTime.Date == date.Date).OrderByDescending(m => m.Title).ToList();
                    }
                    break;
                case "DateTime":
                    if (sortDir == "Ascend")
                    {
                        marks = db.Marks.Where(m => m.DateTime.Date == date.Date).OrderBy(m => m.DateTime).ToList();
                    }
                    else
                    {
                        marks = db.Marks.Where(m => m.DateTime.Date == date.Date).OrderByDescending(m => m.DateTime).ToList();
                    }
                    break;
            }
            if (searchline != null)
            {
                marks = marks.Where(m => m.Title.Contains(searchline)).ToList();
            }
            return View(marks);
        }


        public ActionResult Create(int year = 0, int month = 0, int day = 0)
        {
            if(year == 0)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
                day = DateTime.Now.Day;
            }
            ViewBag.DateTime = new DateTime(year, month, day);
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(string title, string description, DateTime date, DateTime time)
        {
            try
            {
                DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
                db.Marks.Add(new Mark(title, description, dateTime));
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
            Mark mk = db.Marks.Single(m => m.Id == id);
            if(mk == null)
            {
                return NotFound();
            }

            return View(mk);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string title, string description, DateTime date, DateTime time)
        {
            try
            {
                DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
                db.Marks.Update(new Mark(id, title, description, dateTime));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex;
                return RedirectToPage("ErrorPage");
            }

            return Redirect("Index");
        }


        public ActionResult Delete(int id)
        {
            Mark mk = db.Marks.Single(m => m.Id == id);
            if (mk == null)
            {
                return NotFound();
            }

            db.Marks.Remove(mk);
            db.SaveChanges();
            return Redirect("Index");
        }
    }
}
