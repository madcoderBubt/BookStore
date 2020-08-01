using BookStore.Data.Interface;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class GuestMessageRepo : IGuestMessage
    {
        private BookStoreContext _storeContext;

        public GuestMessageRepo(BookStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IEnumerable<GuestMessage> GuestMessages => _storeContext.GuestMessages;

        public IEnumerable<GuestMessage> GetMessageByEmail(string email) => _storeContext.GuestMessages.Where(f => f.Email == email);

        public GuestMessage GuestMessageById(int id) => _storeContext.GuestMessages.Find(id);

        public bool SendMessage(GuestMessage message)
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
