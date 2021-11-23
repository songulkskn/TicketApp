using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class ManagerRepository
    {
        private readonly TicketDbContext _db;
        public ManagerRepository()
        {
            _db = new TicketDbContext();
        }
        public void Add(Manager manager)
        {

        }

        public Manager Find(string id)
        {
            return _db.Managers.Find(id);
        }

        public List<Manager> List()
        {
            return _db.Managers.ToList();
        }

        public void Delete(string id)
        {

        }

        public void Update(Manager M)
        {

        }
    }
}
