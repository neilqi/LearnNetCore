﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            //List<string> results = new List<string>();
            //foreach (Product p in Product.GetProducts())
            //{
            //    string name = p?.Name ?? "<No Name>";
            //    decimal? price = p?.Price ?? 0;
            //    string relatedName = p?.Related?.Name ?? "<None>";
            //    results.Add($"Name:{name}, Price:{price}, Related Name:{relatedName}");
            //}
            //return View(results);

            //Dictionary<string, Product> products = new Dictionary<string, Product>
            //{
            //    { "Kayak", new Product{ Name = "Kayak", Price = 275M} },
            //    { "Lifejacket", new Product(false){Name = "Lifejacket", Price = 48.95M } }
            //};

            //Dictionary<string, Product> products = new Dictionary<string, Product>
            //{
            //    ["Kayak"] = new Product{ Name = "Kayak", Price = 275M},
            //    ["Lifejacket"] = new Product(false) { Name = "Lifejacket", Price = 48.95M }
            //};
            //return View("Index", products.Keys);

            //object[] data = new object[] { 275M, 29, 95M, "apple", "orange", 100, 10 };
            //decimal total = 0;
            //for (int i = 0; i < data.Length; i++)
            //{
            //    if (data[i] is decimal d)
            //    {
            //        total += d;
            //    }
            //}
            //return View("Index", new string[] { $"Total:{total:C2}" });

            //object[] data = new object[] { 275M, 29, 95.5M, "apple", "orange", 100, 10 };
            //decimal total = 0;
            //for (int i = 0; i < data.Length; i++)
            //{
            //    switch (data[i])
            //    {
            //        case decimal decimalValue:
            //            total += decimalValue;
            //            break;
            //        case int intValue when intValue > 50:
            //            total += intValue;
            //            break;
            //    }
            //}
            //return View("Index", new string[] { $"Total:{total:C2}" });

            //ShoppingCart card = new ShoppingCart { products = Product.GetProducts() };
            //decimal cartTotal = card.TotalPrices();
            //return View("Index", new string[] { $"Total: {cartTotal:C2}" });

            //ShoppingCart cart = new ShoppingCart { products = Product.GetProducts() };
            //Product[] productArray = {
            //    new Product {Name = "Kayak", Price = 275M},
            //    new Product {Name = "Lifejacket", Price = 48.95M}
            //};
            //decimal cartTotal = cart.TotalPrices();
            //decimal arrayTotal = productArray.TotalPrices();

            //return View("Index", new string[] {
            //            $"Cart Total: {cartTotal:C2}",
            //            $"Array Total: {arrayTotal:C2}" });


            Product[] productArray =
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                };

            //decimal priceFilterTotal = productArray.FilterByPrice(20).TotalPrices();
            //decimal nameFilterTotal = productArray.FilterByName('S').TotalPrices();
            //return View("Index", new string[] {
            //        $"Price Total: {priceFilterTotal:C2}",
            //        $"Name Total: {nameFilterTotal:C2}" });

            Func<Product, bool> nameFilter = delegate (Product prod)
            {
                return prod?.Name?[0] == 'S';
            };

            //以下三种方式等价
            decimal priceFilterTotal = productArray.Filter(FilterByPrice).TotalPrices();
            decimal priceFilterTotal2 = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            decimal nameFileterTotal = productArray.Filter(nameFilter).TotalPrices();
            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFileterTotal:C2}" });
        }

        bool FilterByPrice(Product p)
        {
            return (p?.Price ?? 0) >= 20;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
