using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using WebApplication3.TicketShop.Domain.DomainModels;

namespace TicketShop.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
