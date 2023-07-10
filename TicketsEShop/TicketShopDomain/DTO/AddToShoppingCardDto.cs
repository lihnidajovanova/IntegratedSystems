using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.TicketShop.Domain.DomainModels;

namespace WebApplication3.TicketShop.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Ticket SelectedTicket { get; set; }

        public Guid TicketId { get; set; }

        public int Quantity { get; set; }
    }
}
