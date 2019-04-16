using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
