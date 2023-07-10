using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using WebApplication3.TicketShop.Domain;
using WebApplication3.TicketShop.Domain.Identity;

namespace WebApplication3.TicketShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {

        public string UserId { get; set; }

        public TicketShopApplicationUser User { get; set; }

        public virtual ICollection<TicketInOrder> Tickets { get; set; }
    }
}
