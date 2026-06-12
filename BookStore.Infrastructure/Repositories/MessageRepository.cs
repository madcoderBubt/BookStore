using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private BookStore.Infrastructure.Data.BookStoreContext _storeContext;

        public MessageRepository(BookStore.Infrastructure.Data.BookStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IEnumerable<Message> Messages => _storeContext.Messages;

        public IEnumerable<Message> GetMessageByEmail(string email) => _storeContext.Messages.Where(f => f.Email == email);

        public Message MessageById(int id) => _storeContext.Messages.Find(id);

        public bool SendMessage(Message message)
        {
            if(message != null)
            {
                try
                {
                    _storeContext.Add(message);
                    _storeContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                     return false;
                }
            }
            return false;
        }
    }
}
