using System;
using System.Collections.Generic;
using System.Text;
using WebApplication3.TicketShop.Domain.DomainModels;
using WebApplication3.TicketShop.Domain.DTO;

namespace TicketShop.Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();

        Ticket GetDetailsForTicket(Guid? id);

        void CreateNewTicket(Ticket t);

        void UpdateExistingTicket(Ticket t);

        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);

        void DeleteTicket(Guid id);

        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
