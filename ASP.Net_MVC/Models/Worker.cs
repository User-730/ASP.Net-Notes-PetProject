//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

using ASP.Net_MVC.DataBases;
using System.ComponentModel.DataAnnotations;

namespace ASP.Net_MVC.Models
{
    public class Worker
    {
        public static void FillEventsAndWorkers(MainDbContext db)
        {
            if (db.Events.Count() == 0)
            {
                List<EventAction> events = new List<EventAction>
                    {
                        {new EventAction("Отправление", new DateTime(2024, 8, 8), new Worker("Олег", "Николаев", "middle"), "Событие1")},
                        {new EventAction("Изменение", new DateTime(2024, 9, 12), new Worker("Анна", "Фролова", "senior"), "Событие2")},
                        {new EventAction("Удаление",new DateTime(2024, 7, 24), new Worker("Федор", "Сергеев", "junior"), "Событие3")},
                        {new EventAction("Создание", new DateTime(2024, 9, 30), new Worker("Муса", "Исаев", "lead"), "Событие4")}
                    };

                db.Events.AddRange(events);
                db.SaveChanges();
            }
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; } //Должность работника
        public List<EventAction>? Actions { get; set; }

        public Worker(string FirstName = "unknown", string LastName = "unknown", string Position = "unknown")
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Position = Position;
        }

        public Worker(int Id, string FirstName = "unknown", string LastName = "unknown", string Position = "unknown")
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Position = Position;
        }
    }
}
