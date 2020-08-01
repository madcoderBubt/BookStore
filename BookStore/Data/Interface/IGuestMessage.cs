using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    public interface IGuestMessage
    {
        IEnumerable<GuestMessage> GuestMessages { get; }

        bool SendMessage(GuestMessage message);
        IEnumerable<GuestMessage> GetMessageByEmail(String email);
        GuestMessage GuestMessageById(int id);
    }
}
