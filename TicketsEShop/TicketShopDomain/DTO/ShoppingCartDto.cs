using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.TicketShop.Domain.DomainModels;

namespace WebApplication3.TicketShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> TicketsInShoppingCart { get; set; }

        public double TotalPrice { get; set; }
    }
}
