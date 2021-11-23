using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class EmployeeRepository
    {

        private readonly TicketDbContext _db;
        public EmployeeRepository()
        {
            _db = new TicketDbContext();
        }
        public void Add(Employee employee)
        {

        }

        public Employee Find(string id)
        {
            return _db.Employees.Find(id);
        }

        public List<Employee> List()
        {
            return _db.Employees.ToList();
        }

        public void Delete(string id)
        {

        }

        public void Update(Employee E)
        {

        }
    }
}
