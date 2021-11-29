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
    public class CompletedTicketModel : PageModel
    {
        private readonly TicketRepository tRepo;
        private readonly CustomerRepository cRepo;
        private readonly TicketService ticketService;
        private readonly EmployeeRepository eRepo;
        private readonly NetSmtpMailService mailService;

        [BindProperty]

        public Ticket Ticket { get; set; }

        [BindProperty]
        public Employee EmployeeInput { get; set; }

        [BindProperty]
        public string selectedcustomerid { get; set; }

        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public List<Ticket> TicketInputs { get; set; }
        public Manager Manager { get; set; } = new Manager();

        [BindProperty]
        public string ID { get; set; }

        public CompletedTicketModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, EmployeeRepository eRepo, NetSmtpMailService mailService)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.eRepo = eRepo;
            this.mailService = mailService;
        }

        public void OnGet()
        {

            Tickets = tRepo.List();

            if (Tickets.Count != 0)
            {
                foreach (var item in Tickets)
                {
                    if (item.Status == StatusType.Completed)
                    {
                        TicketInputs.Add(item);
                    }
                }
            }
            mailService.SendEmail(from: Manager.EMail, to: EmployeeInput.EMail, message: $"{ Ticket.Id} nolu Task Kapatýlmýþtýr.", subject: Ticket.Subject);


        }
    }
}



