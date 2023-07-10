using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace WebApplication3.TicketShop.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int TicketPrice { get; set; }

        public DateTime DateValid { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCart { get; set; }

        public virtual ICollection<TicketInOrder> Orders { get; set; }


    }
}
