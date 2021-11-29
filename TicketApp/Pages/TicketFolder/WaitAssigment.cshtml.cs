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
    public class WaitAssigmentModel : PageModel
    {

        private readonly TicketRepository tRepo;
        private readonly CustomerRepository cRepo;
        private readonly TicketService ticketService;
        private readonly EmployeeRepository eRepo;

        [BindProperty]
        public List<Ticket> TicketInput { get; set; } = new List<Ticket>();


        public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        public Ticket Ticket { get; set; } = new Ticket();


        [BindProperty]
        public Employee EmployeeInput { get; set; }
        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        public string TicketID { get; set; }





        public WaitAssigmentModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService, EmployeeRepository eRepo)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;
            this.eRepo = eRepo;



        }

        public void OnGet()
        {


            var Employees = eRepo.List();

            SelectListItems = Employees.Select(a =>
              new SelectListItem
              {
                  Value = a.Id,
                  Text = a.Name
              }).ToList();

            Tickets = tRepo.List();
            if (Tickets.Count != 0)
            {
                foreach (var item in Tickets)
                {
                    if (item.Status == StatusType.ReadyForAssigned)
                    {
                        TicketInput.Add(item);
                    }
                }
            }


        }

        public void OnPostSave(string ID, string ticketid)
        {

            //emp id
            ID = ID;

            TicketID = ticketid;

            Ticket = tRepo.Find(ticketid);
            EmployeeInput = eRepo.Find(ID);
            Ticket.Status = StatusType.Assigned;
            Ticket.EmployeeId = ID;
            Ticket.AssigneDate = DateTime.Now.Date;
            tRepo.Update(Ticket);
            ticketService.AssingnedTask(ticket: Ticket, empId: EmployeeInput.Id, emp: EmployeeInput);
            ticketService.SetHours(employee: EmployeeInput, ticket: Ticket);


        }

    }
}

