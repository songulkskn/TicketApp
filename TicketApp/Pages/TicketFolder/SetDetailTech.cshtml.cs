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
    public class SetDetailTechModel : PageModel
    {

        private readonly TicketRepository tRepo;
        private readonly CustomerRepository cRepo;
        private readonly TicketService ticketService;
        
        [BindProperty]
        public Ticket TicketInput { get; set; } = new Ticket();
       
        [BindProperty]
        public string ID { get; set; }


        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public string CustomerID { get; set; }
        [BindProperty]
        public LevelofPriority Priority { get; set; }
        [BindProperty]
        public LevelofDifficulty Difficulty { get; set; }

 

        private Array  Listpriority = Enum.GetValues(typeof(LevelofPriority));
        

        private Array  Listdifficulty = Enum.GetValues(typeof(LevelofDifficulty));




        public SetDetailTechModel(TicketRepository tRepo, CustomerRepository cRepo, TicketService ticketService)
        {
            this.tRepo = tRepo;
            this.cRepo = cRepo;
            this.ticketService = ticketService;

        }
       
        public void OnGet(string id)
        {
            ID = id;
            ViewData["Id"] = id;
            TicketInput = tRepo.Find(id);
            CustomerID = TicketInput.CustomerId.ToString();
        }
       
        public void OnPostSetPrio( string id ,LevelofPriority prio)
        {
           
            TicketInput = tRepo.Find(ID);

            ticketService.SetPriority(TicketInput,Priority);
           
        }

        public void OnPostSetDiff()
        {
        
            TicketInput = tRepo.Find(ID);

            ticketService.SetDifficulty(TicketInput, Difficulty);
           
           
        }

       

    }
}
