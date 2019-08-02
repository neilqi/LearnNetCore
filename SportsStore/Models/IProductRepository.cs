using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //使用接口的目的：依赖于该接口的类获取Product对象时无需考虑存储细节
    public interface IProductRepository
    {
        // 通过实现Iquerable接口，linq转化为sql语句时会带上where子句
        // 需要注意的是，每次枚举IQueryable对象时，都会查库
        // 在这种情况下，可以将IQuerable对象转为List或Array
        IQueryable<Product> Products { get; }
    }
}
