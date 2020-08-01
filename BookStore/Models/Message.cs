using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class GuestMessage : SharedModel.Entity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Website { get; set; }
        public String Subject { get; set; }
        public String Details { get; set; }
    }
}
