using ASP.Net_MVC.DataBases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.Net_MVC.Models
{
    
    public class EventAction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime EventDate { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }

        public EventAction(string Type, DateTime EventDate, Worker Worker, string Title = "unknown")
        {
            this.Title = Title;
            this.Type = Type;
            this.EventDate = EventDate;
            this.Worker = Worker;
        }

        public EventAction(int Id, string Type, DateTime EventDate, Worker Worker, string Title = "unknown")
        {
            this.Id = Id;
            this.Title = Title;
            this.Type = Type;
            this.EventDate = EventDate;
            this.Worker = Worker;
        }

        public EventAction(string Type, DateTime EventDate, int WorkerId, string Title = "unknown")
        {
            this.Title = Title;
            this.Type = Type;
            this.EventDate = EventDate;
            this.WorkerId = WorkerId;
        }

        public EventAction(int Id, string Type, DateTime EventDate, int WorkerId, string Title = "unknown")
        {
            this.Id = Id;
            this.Title = Title;
            this.Type = Type;
            this.EventDate = EventDate;
            this.WorkerId = WorkerId;
        }

        public EventAction()
        {

        }
    }
}
