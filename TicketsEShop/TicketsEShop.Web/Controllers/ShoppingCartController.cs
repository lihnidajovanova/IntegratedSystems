using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketShop.Repository;
using TicketShop.Service.Interface;
using WebApplication3.TicketShop.Domain.DomainModels;
using WebApplication3.TicketShop.Domain.DTO;
using WebApplication3.TicketShop.Domain.Identity;

namespace WebApplication3.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this._shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._shoppingCartService.GetShoppingCartInfo(userId));
        }

       
        public IActionResult DeleteTicketFromShoppingCart(Guid ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.DeleteTicketFromShoppingCart(userId, ticketId);

            if(result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            } else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.GetShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Ticket EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if(charge.Status == "succeeded")
            {
                var result = this.OrderNow();
                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
            }
            return RedirectToAction("Index", "ShoppingCart");

        }

        private bool OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.OrderNow(userId);

            return result;
        }
    }
}
