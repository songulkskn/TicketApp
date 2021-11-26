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
        EmployeeRepository _employeeRepository;

        public TicketService(TicketRepository ticketRepository , EmployeeRepository employeeRepository)
        {
            _ticketRepository = ticketRepository;
            _employeeRepository = employeeRepository;


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
            
            
            _ticketRepository.Add(ticket);
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

        /// <summary>
        /// Emplooye ye iş atama işlemleri yapılır ve validatasyonlar yapılır.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="empId"></param>
        public void AssingnedTask(Ticket ticket ,string empId)
        {

            var employee = _employeeRepository.Find(empId);
            if ((int)ticket.Difficulty == 0)
            {
                throw new Exception("the degree of difficulty cannot be passed blank");
            } 
            if ((int)ticket.Priority == 0)
            {
                throw new Exception("the degree of priority cannot be passed blank");
            }

            int count = 0;
            foreach( var item in employee.Ticket)
            {
                if(item.Difficulty == LevelofDifficulty.Hard)
                {
                    count++;
                    if(count > 3)
                    {
                        throw new Exception("no more than 3 tasks can be assigned");
                    }
                }
                if (employee.Hours > 160)
                {
                    throw new Exception("no more than 160 hours of task can be assigned");
                }

                employee.Ticket.Add(ticket);
                _employeeRepository.Update(employee);
            }

            ticket.EmployeeId = empId;
            _ticketRepository.Update(ticket);


        }
        public void SetPriority(Ticket ticket, LevelofPriority priority)
        {
            ticket.Priority = priority;
            _ticketRepository.Update(ticket);
                
        }
        public void SetDifficulty(Ticket ticket ,LevelofDifficulty difficulty)
        {
            
            ticket.Difficulty = difficulty;
            _ticketRepository.Update(ticket);
        }



        public void SetHours (Employee employee, Ticket ticket)
        {

            int WorkHour;

            if ((int)ticket.Priority == 5)
            {
                WorkHour = 5 * 8;
            } 
            if ((int)ticket.Priority == 4)
            {
                WorkHour = 4 * 8;
            }
            if ((int)ticket.Priority == 3)
            {
                WorkHour = 3 * 8;
            }
            if ((int)ticket.Priority == 2)
            {
                WorkHour = 2 * 8;
            } 
            if ((int)ticket.Priority == 1)
            {
                WorkHour =  8;
            }

             WorkHour=employee.Hours;
           
        }

    }
}
