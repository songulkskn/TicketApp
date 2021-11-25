using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketApp.Models;
using TicketApp.Repository;
using TicketApp.Services;

namespace TicketApp.Pages.TicketFolder
{
    public class TicketFormModel : PageModel
    {
        private readonly CustomerRepository cRepo;
        private readonly TicketRepository tRepo;
        private readonly TicketService ticketservice;
     


        public TicketFormModel(CustomerRepository cRepo, TicketRepository tRepo, TicketService ticketservice)
        {
            this.cRepo = cRepo;
            this.tRepo = tRepo;
            this.ticketservice = ticketservice;
            
        }
        [BindProperty]
        public Ticket TicketInput { get; set; }

        public List<SelectListItem> SelectListItems = new List<SelectListItem>();
        public void OnGet()
        {
            var Customers = cRepo.List();

            SelectListItems = Customers.Select(a =>
              new SelectListItem
              {
                  Value = a.Id,
                  Text = a.Name
              }).ToList();
        }
        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {

                TicketInput.OpenDate = DateTime.Now;
                TicketInput.Status = StatusType.Open;
                ticketservice.CreateTicket(TicketInput);

                tRepo.Save();
               

            }

        }



    }
}
