using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;
using TicketShop.Service.Interface;
using WebApplication3.TicketShop.Domain.DomainModels;
using WebApplication3.TicketShop.Domain.DTO;

namespace TicketShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _messageRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<TicketInOrder> ticketInOrderRepository, IRepository<EmailMessage> messageRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this._userRepository = userRepository;
            this._orderRepository = orderRepository;
            this._ticketInOrderRepository = ticketInOrderRepository;
            this._messageRepository = messageRepository;
        }

        public bool DeleteTicketFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var ticketToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.TicketId == id).FirstOrDefault();

                userShoppingCart.TicketsInShoppingCart.Remove(ticketToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto GetShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var allTickets = userShoppingCart.TicketsInShoppingCart.ToList();

            var ticketPrice = allTickets.Select(z => new
            {
                TicketPrice = z.Ticket.TicketPrice,
                Quantity = z.Quantity
            }).ToList();

            int total = 0;

            foreach (var item in ticketPrice)
            {
                total += item.TicketPrice * item.Quantity;
            }

            ShoppingCartDto shoppingCartDtoItem = new ShoppingCartDto
            {
                TicketsInShoppingCart = userShoppingCart.TicketsInShoppingCart.ToList(),
                TotalPrice = total
            };

            return shoppingCartDtoItem;
        }

        public bool OrderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Successfully created order";
                message.Status = false;


                Order orderItem = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser
                };

                this._orderRepository.Insert(orderItem);

                List<TicketInOrder> ticketsInOrder = new List<TicketInOrder>();
                var result = userCart.TicketsInShoppingCart
                    .Select(z => new TicketInOrder
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderItem.Id,
                        TicketId = z.Ticket.Id,
                        SelectedTicket = z.Ticket,
                        UserOrder = orderItem,
                        Quantity = z.Quantity
                    }).ToList();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Your order is completed. The order contains");

                var totalPrice = 0.0;

                for (int i = 1; i < result.Count(); i++)
                {
                    var item = result[i-1];
                    totalPrice += item.Quantity * item.SelectedTicket.TicketPrice;
                    sb.AppendLine(i.ToString() + " " + item.SelectedTicket.MovieName + " with price of: " + item.SelectedTicket.TicketPrice + " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total Price: " + totalPrice.ToString());
                message.Content = sb.ToString();
                
                ticketsInOrder.AddRange(result);

                foreach (var item in ticketsInOrder)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.TicketsInShoppingCart.Clear();
                this._messageRepository.Insert(message);
                this._userRepository.Update(loggedInUser);
                return true;
            }
            return false;
            
        }
    }
}
