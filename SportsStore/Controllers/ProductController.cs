using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
            
        }


        //public ViewResult List(int productPage = 1)
        //{
        //    return View(repository.Products
        //        .OrderBy(p => p.ProductID)
        //        .Skip((productPage - 1) * PageSize)
        //        .Take(PageSize));
        //}
        // 改用ProductListViewModel类传递分页的详细信息和待展示的产品列表
        public ViewResult List(string category, int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.ProductID)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                                repository.Products.Count() :
                                repository.Products.Where(e =>
                                e.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}