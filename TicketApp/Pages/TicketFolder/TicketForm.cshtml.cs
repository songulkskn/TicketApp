using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketApp.Models;
using TicketApp.Repository;

namespace TicketApp.Pages.TicketFolder
{
    public class TicketFormModel : PageModel
    {
        private readonly CustomerRepository cRepo;
        private readonly TicketRepository tRepo;

        public TicketFormModel(CustomerRepository cRepo, TicketRepository tRepo)
        {
            this.cRepo = cRepo;
            this.tRepo = tRepo;
        }
        [BindProperty]
        public Ticket TicketInput { get; set; }

        [BindProperty]
        public List<Customer> CustomerInput { get; set; } = new List<Customer>();

        public void OnGet()
        {
           var  customer = cRepo.List();

        }
    }
}
