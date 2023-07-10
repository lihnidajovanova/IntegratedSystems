using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;
using WebApplication3.TicketShop.Domain.DomainModels;

namespace TicketShop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.Tickets)
                .Include(z => z.User)
                .Include("Tickets.SelectedTicket")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                            .Include(z => z.Tickets)
                            .Include(z => z.User)
                            .Include("Tickets.SelectedTicket")
                            .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
