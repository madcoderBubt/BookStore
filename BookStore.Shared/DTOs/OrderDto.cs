using System.ComponentModel.DataAnnotations;

namespace BookStore.Shared.DTOs
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Pls, Enter First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pls, Enter Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pls, Provide Address.")]
        public string Address { get; set; }

        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Enter your Email.")]
        public string Email { get; set; }
    }
}
