using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Order : Entity
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Pls, Enter First Name.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Pls, Enter First Name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pls, Provide Address.")]
        public string Address { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Enter your Email.")]
        public string Email { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderedPlaced { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

    }
}
