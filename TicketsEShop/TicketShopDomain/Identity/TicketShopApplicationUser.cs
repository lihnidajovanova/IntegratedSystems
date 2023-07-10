using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.TicketShop.Domain.DomainModels;

namespace WebApplication3.TicketShop.Domain.Identity
{
    public class TicketShopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public virtual ShoppingCart UserCart { get; set; }
    }
}
