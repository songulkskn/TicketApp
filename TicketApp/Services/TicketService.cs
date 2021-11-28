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
        /// <param name="employee"></param>
        public void AssingnedTask(Ticket ticket ,string empId, Employee emp)
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

            int countdiff = 0;
            int countprio = 0;
            foreach( var item in employee.Ticket)
            {
                if(item.Difficulty == LevelofDifficulty.Hard)
                {
                    countdiff++;

                    if(countdiff > 3)
                    {
                        throw new Exception("no more than 3 tasks can be assigned");
                    }
                    if (item.OpenDate > DateTime.Now.Date)
                    {
                          throw new Exception("There are 3 difficult tasks in a month, you cannot assign them");
                    }
                }



                if (item.Priority == LevelofPriority.Forth || item.Priority == LevelofPriority.Fifth)
                {
                
                countprio++;
                  if (countprio==5)
                    {
                        throw new Exception("You cannot assign it, it has 4-5 degree of importance");
                    }
                
                
                }

                if (employee.Hours > 160 || item.OpenDate > DateTime.Now.Date)
                {
                    throw new Exception("You cannot assign working hours are full");
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

         public void SetHours(Employee employee, Ticket ticket) 
        { 

            int hour;

            if ((int)ticket.Priority ==5 )
            {
                hour = 8 * 5;
            }

            if ((int)ticket.Priority == 4)
            {
                hour = 8 * 4;
            }
            if ((int)ticket.Priority == 3)
            {
                hour = 8 * 3;
            }
            if ((int)ticket.Priority == 2)
            {
                hour = 8 * 2;
            }
            if ((int)ticket.Priority == 1)
            {
                hour = 8 * 1;
            }

            hour = employee.Hours;

            _employeeRepository.Update(employee);


        }
      
        
    }
}
