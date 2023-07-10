using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using WebApplication3.TicketShop.Domain.Identity;

namespace WebApplication3.TicketShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }

        public TicketShopApplicationUser Owner { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
