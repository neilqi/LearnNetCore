using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnitTest.Models;

namespace XUnitTest
{
    public class ProductTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GetPriceUnder50() };
            yield return new object[] { GetPricesOver50 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //使用方法定义测试数据
        private IEnumerable<Product> GetPriceUnder50()
        {
            decimal[] prices = new decimal[] { 276, 49.95M, 19.50M, 24.95M };
            for (int i = 0; i < prices.Length; i++)
            {
                yield return new Product { Name = $"P{i + 1}", Price = prices[i] };
            }
        }

        //使用属性定义测试数据
        private Product[] GetPricesOver50 => new Product[] 
        {
            new Product { Name = "P1", Price = 5 },
            new Product { Name = "P2", Price = 48.95M },
            new Product { Name = "P3", Price = 19.50M },
            new Product { Name = "P4", Price = 24.95M }
        };
    }
}
