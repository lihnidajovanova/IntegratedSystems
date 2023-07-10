﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace WebApplication3.TicketShop.Domain.DomainModels
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public Guid ShoppingCartId { get; set; }

        public int Quantity { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
