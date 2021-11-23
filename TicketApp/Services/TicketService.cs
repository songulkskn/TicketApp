using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketApp.Models;
using TicketApp.Repository;

namespace TicketApp.Services
{
    public class TicketService
    {
        TicketRepository _ticketRepository;

        public TicketService(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }


        /// <summary>
        ///  Ticket oluşturulurken uyulması gereken validate kuralları
        /// </summary>
        /// <param name="ticket"></param>
        public void CreateTicket(Ticket ticket)
        {
            if (ticket.Subject == null)
            {
                throw new Exception("the subject field cannot be passed blank");
            }
            if (ticket.Subject.Length > 50) 
            {
                throw new Exception("the subject cannot exceed 50 characters");
            }
            if (ticket.Description == null)
            {
                throw new Exception("the description field cannot be passed blank");
            }
            if (ticket.Description.Length > 500) 
            {
                throw new Exception("the subject cannot exceed 500 characters");
            }
        }
        /// <summary>
        /// gelen statusa göre ticket tablosundaki status değeri güncellenir.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="status"></param>
        public void UpdateStatus(Ticket ticket ,StatusType status)
        {
            ticket.Status = status;
            _ticketRepository.Update(ticket);
        }
        public void AssingnedTask(Ticket ticket ,string empId)
        {
            // burada emp lere task atanacak
        }
        public void SetPriority()
        {
            //
        } 
        public void SetDifficulty()
        {
            //
        }

    }
}
