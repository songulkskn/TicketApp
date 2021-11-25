using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketApp.Models;
using TicketApp.Repository;
using TicketApp.Services;

namespace TicketApp.Pages.TicketFolder
{
    public class OpenTicketModel : PageModel
    {


        private readonly TicketRepository tRepo;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public List<Ticket> TicketInput { get; set; } = new List<Ticket>();


        public OpenTicketModel( TicketRepository tRepo)
        {
            this.tRepo = tRepo;
          

        }


        public void OnGet()
        {
            Tickets = tRepo.List();
            if (Tickets.Count != 0)
            {
                foreach(var item in Tickets)
                {
                    if (item.Status == StatusType.Open)
                    {
                        TicketInput.Add(item);
                    }
                }
            }

        }


       
    }
}
