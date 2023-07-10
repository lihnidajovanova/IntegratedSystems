using System;
using System.Collections.Generic;
using System.Text;
using WebApplication3.TicketShop.Domain.DTO;

namespace TicketShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartInfo(string userId);

        bool DeleteTicketFromShoppingCart(string userId, Guid id);

        bool OrderNow(string userId);
    }
}
