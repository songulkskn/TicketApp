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
    public class AssignedTicketModel : PageModel
    {


        private readonly TicketRepository tRepo;
        private readonly CustomerRepository cRepo;
        private readonly TicketService ticketService;
        private readonly EmployeeRepository eRepo; 
     



        [BindProperty]

        public Ticket Ticket { get; set; }

        [BindProperty]
        public Employee EmployeeInput { get; set; }

        [BindProperty]
        public string selectedcustomerid { get; set; }

        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public List<Ticket> TicketInputs { get; set; } = new List<Ticket>();

        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

        public AssignedTicketModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, EmployeeRepository eRepo)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.eRepo = eRepo;
        }

        public void OnGet()
        {
            
            Tickets = tRepo.List();

            var employeerep = eRepo.List();

            if (Tickets.Count != 0)
            {
                foreach (var item in Tickets)
                {
                    if (item.Status == StatusType.Assigned)
                    {
                        TicketInputs.Add(item);
                    }
                    foreach(var item2 in employeerep)
                    {
                        if (item.EmployeeId == item2.Id)
                        {
                            EmployeeList.Add(item2);
                        }
                    }
                }
            }

        }

        public void OnPostCloseTicket(string id)
        {

            ID = id;

            Ticket = tRepo.Find(id);

            EmployeeInput = eRepo.Find(Ticket.EmployeeId);

            Ticket.CloseDate = DateTime.Now.Date;

            Ticket.Status = StatusType.Closed;
            tRepo.Update(Ticket);

        }
    }
}
