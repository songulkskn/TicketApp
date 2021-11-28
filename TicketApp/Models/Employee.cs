using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp.Models
{
    public class Employee
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string EMail { get; set; }
        public int Hours { get; set; }
        public List<Ticket> Ticket { get; set; }
        public  Manager Manager { get; set; }
        public  string ManagerId { get; set; }


    
        
        public void SetTicket(Ticket ticket)
        {
            Ticket.Add(ticket);
        }

        public void SetHours(int hours)
        {
            this.Hours = hours;
        }
    }
}
