using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CustomerRepository
    {
        private readonly TicketDbContext _db;
        public CustomerRepository()
        {
            _db = new TicketDbContext();
        }
        public void Add(Customer customer)
        {
  
        }

        public Customer Find(string id)
        {
            return _db.Customers.Find(id);
        }

        public List<Customer> List()
        {
            return _db.Customers.ToList();
        }

        public void Delete(string id)
        {
       
        }

        public void Update(Customer C)
        {
          
        }

    }
}
