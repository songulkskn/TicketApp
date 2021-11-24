using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TicketRepository
    {
        private readonly TicketDbContext _db;
        public TicketRepository()
        {
            _db = new TicketDbContext();
        }

        public void Add(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            _db.SaveChanges();
        }

        public Ticket Find(string id)
        {
            return _db.Tickets.Find(id);
        }

        public List<Ticket> List()
        {
            return _db.Tickets.ToList();
        }

        public void Delete(string id)
        {
            var entity = Find(id);
            _db.Tickets.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Ticket p)
        {
            _db.Tickets.Update(p);
            _db.SaveChanges();

        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
