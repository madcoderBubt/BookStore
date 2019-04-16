using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Order : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderedPlaced { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

    }
}
