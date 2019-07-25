using System.Collections.Generic;

namespace UnitTest.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        void AddProduct(Product p);
    }
}
