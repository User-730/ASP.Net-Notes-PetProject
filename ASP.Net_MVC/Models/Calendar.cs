using ASP.Net_MVC.DataBases;
using System.ComponentModel.DataAnnotations;

namespace ASP.Net_MVC.Models
{
    public static class Calendar
    {
        public static string[] MonthsNames = 
        { 
            "Январь", "Февраль", "Март",
            "Апрель", "Май", "Июнь", 
            "Июль", "Август", "Сентябрь", 
            "Октябрь", "Ноябрь", "Декабрь"
        };
        public static void FillMarks(MainDbContext db)
        {
            DateOnly basicDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            db.Marks.AddRange(
                new Mark("Date","nothing", new DateTime(basicDate.Year, basicDate.Month,5,  12,30,0)),
                new Mark("Meeting", "Something", new DateTime(basicDate.Year, basicDate.Month,9,  10,0,0)),
                new Mark("Arrangment", "Important", new DateTime(basicDate.Year, basicDate.Month,23,  18,45,0))
                );
            db.SaveChanges();
        }
        public static string FormatToDateString(DateTime dateTime)
        {
            string year = dateTime.Year.ToString(); 
            string month = dateTime.Month.ToString();
            string day = dateTime.Day.ToString();

            if(dateTime.Month < 10) month = "0" + dateTime.Month.ToString();
            if (dateTime.Day < 10) day = "0" + dateTime.Day.ToString();

            return year + "-" + month + "-" + day;
        }
        public static string FormatToTimeString(DateTime dateTime)
        {
            string hour = dateTime.Hour.ToString();
            string minute = dateTime.Minute.ToString();

            if (dateTime.Minute < 10) minute = "0" + dateTime.Minute.ToString();

            return hour + ":" + minute;
        }
        public static List<DateTime> GetDates(int year, int month)
        {
            if (year == 0) { year = DateTime.Today.Year; month = DateTime.Today.Month; }
            else if (month == 0) { year -= 1; month = 12; }
            else if (month == 13) { year += 1; month = 1; }


            List<DateTime> dates = new List<DateTime>();
            int daysInMonth = DateTime.DaysInMonth(year, month);

            DateTime startDate = new DateTime(year, month, 1);
            DateTime lastDate = new DateTime(year, month, daysInMonth);

            for (int day = 1; day <= 7; day++)
            {
                if (startDate.DayOfWeek == DayOfWeek.Monday)
                {
                    if (startDate.Day != 1) { startDate = startDate.AddDays(-7); }
                    break;
                }
                else { startDate = startDate.AddDays(1); }
            }
            for (int day = 1; day <= 7; day++)
            {
                if (lastDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (lastDate.Day != daysInMonth) { lastDate = lastDate.AddDays(7); }
                    break;
                }
                else { lastDate = lastDate.AddDays(-1); }
            }

            while (startDate <= lastDate)
            {
                dates.Add(startDate);
                startDate = startDate.AddDays(1);
            }

            return dates;
        }
    }
    public class Mark
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public Mark(int id,string title, string description, DateTime dateTime)
        {
            Id = id;
            Title = title;
            Description = description;
            DateTime = dateTime;
        }
        public Mark(string title, string description, DateTime dateTime) 
        {
            Title = title;
            Description = description;
            DateTime = dateTime;
        }

        public Mark() { }
    }
}