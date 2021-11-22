using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp.Models
{
    public enum StatusType
    {
        Open=1,
        ReadyForAssigned=2,
        Review=3,
        Completed=4,
        Assigned=5,
        Closed=6

    }
    public enum LevelofDifficulty
    {
        VeryEasy=1,
        Easy=2,
        Medium=3,
        Hard=4,
        VeryHard=5
       

    }
    public class Ticket
    {
        public Ticket(string subject, string descsiption, StatusType status)
        {
            Subject = subject;
            Descsiption = descsiption;
            Status = status;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Subject { get; private set; }
        public string Descsiption { get; private set; }
        public StatusType Status { get; private set; }
        public LevelofDifficulty Difficulty { get; set; }
        public int Priority { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime AssigneDate { get; set; }
        public DateTime ReviweDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
