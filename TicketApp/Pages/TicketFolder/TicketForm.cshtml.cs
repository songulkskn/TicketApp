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
        private readonly NetSmtpMailService mailService;



        public TicketFormModel(CustomerRepository cRepo, TicketRepository tRepo, TicketService ticketservice, NetSmtpMailService mailService)
        {
            this.cRepo = cRepo;
            this.tRepo = tRepo;
            this.ticketservice = ticketservice;
            this.mailService = mailService;


        }
        [BindProperty]
        public Ticket TicketInput { get; set; }

        public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();
        public Manager Manager { get; set } = new Manager();

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
                mailService.SendEmail(from: "songulkeskin99@gmail.com", to: Manager.EMail, message: $"{ TicketInput.Id} nolu Task Oluþturulmuþtur.", subject: TicketInput.Subject);


            }

        }



    }
}
