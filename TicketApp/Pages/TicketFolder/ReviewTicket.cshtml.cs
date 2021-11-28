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



        [BindProperty]

        public Ticket Ticket { get; set; }

        [BindProperty]
        public Employee EmployeeInput { get; set; }

        public Customer CustomerInput { get; set; }

        public Customer displayCustomer { get; set; }

        public List<SelectListItem> SelectListItems = new List<SelectListItem>();

        [BindProperty]
        public string selectedcustomerid { get; set; }

        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public List<Ticket> TicketInputs { get; set; }

        [BindProperty]
        public string ID { get; set; }



        public ReviewTicketModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, EmployeeRepository eRepo)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.eRepo = eRepo;
        }

        public void OnGet()
        {

            Tickets = tRepo.List();

            if (Tickets.Count != 0)
            {
                foreach (var item in Tickets)
                {
                    if (item.Status == StatusType.Review)
                    {
                        TicketInputs.Add(item);
                    }
                }
            }
        }

        public void OnPostCloseTicket(string id)
        {

            ID = id;

            Ticket = tRepo.Find(id);

          

            var employeemail = eRepo.Find(Ticket.EmployeeId);



           // _semdingmail.SendEmail(from: employeemail.Mail, to: "elif@gmail.com", message: $"{TicketInput.Id} nolu Task Closed Task olarak atanmýþtýr", subject: TicketInput.Subject);
        }
    }
}
