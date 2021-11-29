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
    public class ReviewTicketModel : PageModel
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
        public List<Ticket> TicketInputs { get; set; } = new List<Ticket>();
        [BindProperty]
        public Ticket [] ticketList { get; set; }
        public Manager Manager { get; set; } = new Manager();

        [BindProperty]
        public string ID { get; set; }
        [BindProperty]
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();



        public ReviewTicketModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, EmployeeRepository eRepo, NetSmtpMailService mailService)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.eRepo = eRepo;
            this.mailService = mailService;
        }

        public void OnGet()
        {
            var employeerep =   eRepo.List();
            Tickets = tRepo.List();

            if (Tickets.Count != 0)
            {
                foreach (var item in Tickets)
                {
                    if (item.Status == StatusType.Review)
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
               // ticketList = TicketInputs.ToArray();
            }
        }

        public void OnPostCloseTicket(string id)
        {

            ID = id;

            Ticket = tRepo.Find(id);

            Ticket.CompleteDate = DateTime.Now;

            var employeemail = eRepo.Find(Ticket.EmployeeId);

            mailService.SendEmail(from: EmployeeInput.EMail, to: Manager.EMail, message: $"{ Ticket.Id} nolu Task Kapatýlmýþtýr.", subject: Ticket.Subject);




        }
    }
}
