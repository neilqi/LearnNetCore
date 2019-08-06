using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
                                            .Include(o => o.Lines)
                                            .ThenInclude(o => o.Product);

        public void SaveOrder(Order order)
        {
            // AttachRange一般用法：向集合的末尾添加数组
            // context.AttachRange：可以理解为有一个集合，功能是标记未更改的实体。
            // 向这个集合中添加实体，以便在调用SaveChanges()时不会执行任何操作。
            // 本例中的功能就是避免将已存在的产品重新添加到库
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
