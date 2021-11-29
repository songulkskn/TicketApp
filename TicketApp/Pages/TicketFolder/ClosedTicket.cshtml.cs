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
    public class ClosedTicketModel : PageModel
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

        public List<SelectListItem> SelectListItems = new List<SelectListItem>();

        [BindProperty]
        public string selectedcustomerid { get; set; }

        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public List<Ticket> TicketInputs { get; set; }

        [BindProperty]
        public string ID { get; set; }
        [BindProperty]
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();
        public Manager Manager { get; set; } = new Manager();


        public ClosedTicketModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, EmployeeRepository eRepo, NetSmtpMailService mailService)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.eRepo = eRepo;
            this.mailService = mailService;
        }
        public void OnGet()
        {
            var employeerep = eRepo.List();
            Tickets = tRepo.List();

            if (Tickets.Count != 0)
            {
                foreach (var item in Tickets)
                {
                    if (item.Status == StatusType.Closed)
                    {
                        TicketInputs.Add(item);
                    }
                    foreach (var item2 in employeerep)
                    {
                        if (item.EmployeeId == item2.Id)
                        {
                            EmployeeList.Add(item2);
                        }
                    }
                }
            }
        }


        public void OnPostReviewTicket(string id)
        {

            ID = id;

            Ticket = tRepo.Find(id);

            EmployeeInput = eRepo.Find(Ticket.EmployeeId);

            Ticket.CloseDate = DateTime.Now;

            Ticket.Status = StatusType.Review;

            tRepo.Update(Ticket);

            mailService.SendEmail(from: "songulkeskin99@gmail.com", to: EmployeeInput.EMail, message: $"{ Ticket.Id} nolu Task Completed Task olarak atanmýþtýr", subject: Ticket.Subject);


        }

        public void OnPostCompleteTicket(string id)
        {


            ID = id;

            Ticket = tRepo.Find(id);

            EmployeeInput = eRepo.Find(Ticket.EmployeeId);
  
            Ticket.Status = StatusType.Completed;
            Ticket.CompleteDate = DateTime.Now.Date;

            tRepo.Update(Ticket);

            var customer = cRepo.Find(Ticket.CustomerId);

            mailService.SendEmail(from: EmployeeInput.EMail, to: Manager.EMail, message: $"{ Ticket.Id} nolu Task Kapatýlmýþtýr.", subject: Ticket.Subject);

        }

    }
}

