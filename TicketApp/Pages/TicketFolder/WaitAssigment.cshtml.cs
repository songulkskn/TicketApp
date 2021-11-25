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
    public class WaitAssigmentModel : PageModel
    {

        private readonly TicketRepository tRepo;
        private readonly CustomerRepository cRepo;
        private readonly TicketService ticketService;

        [BindProperty]
        public List<Ticket> TicketInputs { get; set; } = new List<Ticket>(); 
        public WaitAssigmentModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, List<Ticket> ticketInput)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.TicketInputs = tRepo.List();

        }

        public void OnGet()
        {
        }
    }
}
