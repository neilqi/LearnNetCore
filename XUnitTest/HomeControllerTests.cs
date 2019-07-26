using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using UnitTest.Controllers;
using UnitTest.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace XUnitTest
{
    public class HomeControllerTests
    {
        #region 单元测试参数化
        // 使用接口，目的是为了实现测试数据参数化。
        // 此前的方法是直接调用被测试项目的类，被测试项目要为不同的测试提供不同的方法.
        // 改用接口后，测试数据可以在测试项目中随意定制。
        // 定制数据的方法有两种：InLineData和ClassData
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product P)
            {

            }
        }

        // 参数化测试的属性改为[Theory]
        // 使用ClassData代替InlineData， 返回数组，可以避免参数个数固定的问题
        // 测试执行时，Xunit会创建一个新的ProductTestData类实例，用于传递测试数据
        [Theory]
        [ClassData(typeof(ProductTestData))]
        //[InlineData(135, 48.95, 19.50, 24.95)]
        //[InlineData(5, 48.95, 19.50, 24.95)]
        public void IndexActionModelsComplete(Product[] products) {
            // Arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = products
            };
            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
                as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products,
                model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }
        #endregion

        //使用Moq传递假数据
        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelsCompleteTestByMoq(Product[] products)
        {
            // Arrange
            var mock = new Mock<IRepository>();
            //SetupGet作用是指定mock.Object要转载的对象类型；Returns的作用是把products值传给mock.Object
            mock.SetupGet(m => m.Products).Returns(products);
            var controller = new HomeController { Repository = mock.Object };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
                as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products,
                model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        #region 用于测试代码是否只执行了一次
        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;

            public IEnumerable<Product> Products
            {
                get {
                    PropertyCounter++;
                    return new[] { new Product { Name = "P1", Price = 100 } };
                }
            }

            public void AddProduct(Product p)
            {
                
            }
        }
        
        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            // Arrange
            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController { Repository = repo };

            // Act
            var result = controller.Index();

            // Assert
            Assert.Equal(1, repo.PropertyCounter);
        }

        [Fact]
        public void RepositoryPropertyCalledOnceByMoq()
        {
            // Arrange
            var mock = new Mock<IRepository>();

            mock.SetupGet(m => m.Products)
                .Returns(new[] { new Product{ Name = "P1", Price = 100} });
            var controller = new HomeController { Repository = mock.Object };
            // Act
            var result = controller.Index();

            // Assert
            mock.VerifyGet(m => m.Products, Times.Once);
        }
        #endregion

        class ModelCompleteFakeRepositoryPriceUnder50 : IRepository {
            public IEnumerable<Product> Products { get; } = new Product[]
            {
                new Product { Name = "P1", Price = 5M },
                new Product { Name = "P2", Price = 48.95M },
                new Product { Name = "P3", Price = 19.50M },
                new Product { Name = "P3", Price = 34.95M }
            };

            public void AddProduct(Product p)
            {

            }
        }

        [Fact]
        public void IndexActionModelIsCompletePricesUnder50()
        {
            // Arrange， 给controller赋值
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepositoryPriceUnder50();

            // Act，通过controller获得view中使用的Model，即view中显示的数据来源
            var model = (controller.Index() as ViewResult)?.Model as IEnumerable<Product>;

            // Assert，比较数据，判断传给view的数据是否一致
            Assert.Equal(controller.Repository.Products, model,
                Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }
    }
}
