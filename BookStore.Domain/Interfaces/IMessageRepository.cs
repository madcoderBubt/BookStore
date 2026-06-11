using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<Message> Messages { get; }

        bool SendMessage(Message message);
        IEnumerable<Message> GetMessageByEmail(string email);
        Message MessageById(int id);
    }
}
